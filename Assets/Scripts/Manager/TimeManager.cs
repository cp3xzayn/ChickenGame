using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    /// <summary> 経過時間を表示するテキスト </summary>
    [SerializeField] Text m_timeText = null;
    /// <summary> 経過時間 </summary>
    private float m_timer;

    void Start()
    {
        m_timeText = GetComponent<Text>();
    }

    void Update()
    {
        if (GameManager.Instance.NowGameState == GameState.Playing)
        {
            m_timer += Time.deltaTime;
            m_timeText.text = m_timer.ToString("F2"); // 小数2桁まで表示する
        }
    }
}
