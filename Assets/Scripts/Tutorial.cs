using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Tutorialを管理するクラス
/// </summary>
public class Tutorial : MonoBehaviour
{
    /// <summary> 表示する文章を格納する </summary>
    [SerializeField] string[] m_sentences = null;
    /// <summary> 現在表示する文章 </summary>
    string m_nowSentence;
    /// <summary> 文章を表示するText </summary>
    [SerializeField] Text m_tutorialText = null;

    /// <summary> 現在表示している文章番号 </summary>
    int m_nowSentenceNum = 0;
    /// <summary> 文章の配列の長さ </summary>
    int m_sentenceLength;
    /// <summary> 文章の配列の長さ </summary>
    public int SentenceLength => m_sentenceLength;


    /// <summary> 回転速度 </summary>
    [SerializeField] float m_turnSpeed = 2.0f;
    /// <summary> 半径 </summary>
    [SerializeField] float m_radius = 1;
    /// <summary> マウスカーソルのRectTransform </summary>
    [SerializeField] RectTransform m_mouseCursor = null;
    /// <summary> XY,YZを示すPointer </summary>
    [SerializeField] RectTransform m_xYZPointer = null;
    /// <summary> 設置完了を示すPointer </summary>
    [SerializeField] GameObject m_endPointer = null;
    /// <summary> 設定画面を開くボタンを示すPointer </summary>
    [SerializeField] GameObject m_settingPointer = null;


    void Start()
    {
        m_sentenceLength = m_sentences.Length;
        SetNextSentence();
    }

    void Update()
    {
        TutorialObjectIndicate();
    }


    /// <summary>
    /// 文章を次の段階にできる時、次の要素数に進める（EventTriggerに設定している）
    /// </summary>
    public void TextUpdate()
    {
        if (m_nowSentenceNum < m_sentences.Length)
        {
            SetNextSentence();
        }
        else
        {
            Debug.Log("Tutorial終了");
            m_nowSentenceNum = 0;
            this.transform.localScale = Vector3.zero;
        }
    }

    /// <summary>
    /// 次の文章をセットする関数
    /// </summary>
    void SetNextSentence()
    {
        m_nowSentence = m_sentences[m_nowSentenceNum];
        m_tutorialText.text = m_nowSentence;
        m_nowSentenceNum++;
    }


    /// <summary>
    /// TutorialのUIの移動、Animationを管理する
    /// </summary>
    void TutorialObjectIndicate()
    {
        // Tutorialの表示している文章に合わせてUIを表示・非表示を決定する
        switch (m_nowSentenceNum)
        {
            case 0:
                Debug.Log("a");
                m_mouseCursor.localScale = Vector3.zero;
                m_xYZPointer.localScale = Vector3.zero;
                m_endPointer.SetActive(false);
                m_settingPointer.SetActive(false);
                break;
            case 2:
                m_mouseCursor.localScale = Vector3.one;
                MoveCircle();
                break;
            case 3:
                m_mouseCursor.localScale = Vector3.zero;
                m_xYZPointer.localScale = Vector3.one;
                break;
            case 4:
                m_xYZPointer.localScale = Vector3.zero;
                m_endPointer.SetActive(true);
                break;
            case 5:
                m_endPointer.SetActive(false);
                m_settingPointer.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// 円形にマウスカーソルのオブジェクトを回転させる
    /// </summary>
    void MoveCircle()
    {
        float x = m_radius * Mathf.Sin(Time.time * m_turnSpeed);
        float y = m_radius * Mathf.Cos(Time.time * m_turnSpeed);

        m_mouseCursor.position += new Vector3(x, y, 0);
    }
}
