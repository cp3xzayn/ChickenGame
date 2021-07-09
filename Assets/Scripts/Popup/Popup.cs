using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] Text m_titleText = null;
    [SerializeField] Text m_bodyText = null;
    [SerializeField] Button m_okButton = null;
    [SerializeField] Button m_cancelButton = null;


    /// <summary>
    /// Popupを初期化する
    /// </summary>
    /// <param name="title">TitleText</param>
    /// <param name="body">BodyText</param>
    /// <param name="okCallback">OKが押されたときの処理</param>
    /// <param name="cancelCallback">Cancelが押されたときの処理</param>
    public void Initialize(string title, string body, Action okCallback, Action cancelCallback)
    {
        m_titleText.text = title;
        m_bodyText.text = body;
        m_okButton.onClick.AddListener(() => okCallback?.Invoke());
        m_cancelButton.onClick.AddListener(() => cancelCallback?.Invoke());

        //別の書き方
        //m_okButton.onClick.AddListener(okCallback.Invoke);
        //m_okButton.onClick.AddListener(() => okCallback());
        //m_okButton.onClick.AddListener(() => okCallback?.Invoke());
    }
}
