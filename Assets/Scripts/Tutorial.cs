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

    void Start()
    {
        m_sentenceLength = m_sentences.Length;
        SetNextSentence();
    }

    /// <summary>
    /// 文章を次の段階にできる時、次の要素数に進める
    /// </summary>
    public void TextUpdate()
    {
        if (m_nowSentenceNum < m_sentences.Length)
        {
            SetNextSentence();
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
