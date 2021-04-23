using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    /// <summary> XYのパネル </summary>
    [SerializeField] GameObject m_xYPanel = null;
    /// <summary> YZのパネル </summary>
    [SerializeField] GameObject m_yZPanel = null;

    [SerializeField] Camera m_camera = null;

    /// <summary> 掴んでるオブジェクトのポジション </summary>
    Vector3 m_objectPos;

    /// <summary> ObjectSetManagerのアタッチされたオブジェクト </summary>
    [SerializeField] GameObject m_objectSetManagerObj = null;
    /// <summary> ObjectSetManager </summary>
    ObjMoveManager m_objectSetManager;

    void Start()
    {
        m_objectSetManager = m_objectSetManagerObj.GetComponent<ObjMoveManager>();
    }

    void Update()
    {
        switch (GameManager.Instance.NowGameState)
        {
            case GameState.Prepare:
                IndicatePanelsForObjectMove();
                break;
            case GameState.Playing:
                break;
            case GameState.End:
                break;
            default:
                break;
        }
    }

    /// <summary> XYかYZのどちらを操作するか決定する（XY:true,YZ:false） </summary>
    bool isWhicXYorYZ = true;

    /// <summary>
    /// オブジェクトを動かすことができる範囲を表示するPanelの処理
    /// </summary>
    void IndicatePanelsForObjectMove()
    {
        m_objectPos = m_objectSetManager.CubePos;
        if (m_objectSetManager.nowSetPhase == SetPhase.XYSet)
        {
            if (isWhicXYorYZ)
            {
                LookPanel(m_xYPanel, true);
                LookPanel(m_yZPanel, false);
                isWhicXYorYZ = false;
            }
        }
        else if (m_objectSetManager.nowSetPhase == SetPhase.YZSet)
        {
            if (!isWhicXYorYZ)
            {
                LookPanel(m_xYPanel, false);
                LookPanel(m_yZPanel, true);
                m_yZPanel.transform.position = new Vector3(m_objectPos.x + 1, 0, 0);
                m_camera.transform.position = new Vector3(m_objectPos.x - 10, 0, 0);
                m_camera.transform.rotation = Quaternion.Euler(0, 90, 0);
                isWhicXYorYZ = true;
            }
        }
    }

    /// <summary>
    /// オブジェクトのアクティブを変更する
    /// </summary>
    /// <param name="panel"></param>
    /// <param name="isActive"></param>
    public void LookPanel(GameObject panel,bool isActive)
    {
        panel.SetActive(isActive);
    }
}
