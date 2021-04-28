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

    /// <summary> Tutorialが終了したか判定する </summary>
    bool isFinished = false;
    /// <summary> Tutorialが終了したか判定する </summary>
    public bool IsFinished => isFinished;

    void Start()
    {
        SetNextSentence();
    }

    public void TextUpdate()
    {
        if (m_nowSentenceNum < m_sentences.Length)
        {
            SetNextSentence();
        }
        else if (m_nowSentenceNum >= m_sentences.Length)
        {
            isFinished = true;
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
}
