using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    /// <summary> Rigidbody </summary>
    private Rigidbody m_rb;
    /// <summary> 初期位置 </summary>
    Vector3 m_startPos;
    /// <summary> 動く時間(Mathf.PingPongの第2引数に設定する) </summary>
    [SerializeField] float m_moveTime = 3f;
    /// <summary> ObjMoveManager </summary>
    ObjMoveManager m_objMoveManager;
    /// <summary> 移動したObjの位置を一度だけ取得するために用いる </summary>
    bool isOneTimeSet = true;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.NowGameState == GameState.Playing)
        {
            if (isOneTimeSet)
            {
                m_objMoveManager = FindObjectOfType<ObjMoveManager>().GetComponent<ObjMoveManager>();
                m_startPos = m_objMoveManager.ObjectPos;
                isOneTimeSet = false;
            }
            // Mathf.PingPong(https://docs.unity3d.com/ja/2018.4/ScriptReference/Mathf.PingPong.html)
            m_rb.MovePosition(new Vector3(m_startPos.x, m_startPos.y + Mathf.PingPong(Time.time, m_moveTime), m_startPos.z));
        }
        else if (GameManager.Instance.NowGameState == GameState.GameClear)
        {
            isOneTimeSet = true;
            this.transform.position = m_startPos;
        }
    }
}
