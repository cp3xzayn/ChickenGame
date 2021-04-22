using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    [SerializeField] GameObject m_xYPanel = null;
    [SerializeField] GameObject m_yZPanel = null;

    Vector3 m_cubePos;


    [SerializeField] GameObject m_objectSetManagerObj = null;
    ObjectSetManager m_objectSetManager;


    void Start()
    {
        m_objectSetManager = m_objectSetManagerObj.GetComponent<ObjectSetManager>();
    }

    bool isWhicXYorYZ = true;

    void Update()
    {
        m_cubePos = m_objectSetManager.CubePos;
        Debug.Log(m_cubePos.x);
        if (m_objectSetManager.nowSetPhase == SetPhase.XYSet)
        {
            if (isWhicXYorYZ)
            {
                LookXYPanel(true);
                LookYZPanel(false);
                isWhicXYorYZ = false;
            }
        }
        else if (m_objectSetManager.nowSetPhase == SetPhase.YZSet)
        {
            if (!isWhicXYorYZ)
            {
                LookXYPanel(false);
                LookYZPanel(true);
                m_yZPanel.transform.position = new Vector3(m_cubePos.x + 1, 0, 0);
                isWhicXYorYZ = true;
            }
        }
    }

    public void LookXYPanel(bool isActive)
    {
        m_xYPanel.SetActive(isActive);
    }

    public void LookYZPanel(bool isActive)
    {
        m_yZPanel.SetActive(isActive);
        
    }
}
