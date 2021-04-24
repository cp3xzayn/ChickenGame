using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject m_canvas = null;
    [SerializeField] GameObject m_jumpButton = null;

    void Start()
    {
        GameObject jumpButton = Instantiate(m_jumpButton) as GameObject;
        jumpButton.transform.SetParent(m_canvas.transform, false);
    }
}
