using System;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    private static PopupManager instance;

    /// <summary>
    /// ここからPopupManagerにアクセスする
    /// </summary>
    public static PopupManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject singleton = new GameObject();
                instance = singleton.AddComponent<PopupManager>();
                singleton.name = "PopupManager";

                DontDestroyOnLoad(singleton);
            }
            return instance;
        }
    }

    /// <summary>
    /// インスタンスにアクセスできないようにする(private)
    /// </summary>
    private PopupManager() { }

    /// <summary> Canvas </summary>
    Canvas canvas;
    /// <summary>  </summary>
    Popup m_popup;

    void Awake()
    {
        GameObject canvasObject = new GameObject("PopupCanvas");
        canvasObject.transform.SetParent(this.transform);
        canvas = canvasObject.AddComponent<Canvas>();
        canvasObject.AddComponent<GraphicRaycaster>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

    }

    /// <summary>
    /// Popupが開いたときの処理
    /// </summary>
    /// <param name="title"></param>
    /// <param name="body"></param>
    public void Open(string title, string body, Action onClickOK)
    {
        Popup popupPrefab = Resources.Load<Popup>("Popup");
        m_popup = Instantiate(popupPrefab, canvas.transform);
        
        m_popup.Initialize(title, body, onClickOK, null);
    }


    /// <summary>
    /// Popupを閉じたときの処理
    /// </summary>
    /// <param name="onClickCancel"></param>
    public void Close(Action onClickCancel)
    {
        m_popup.Initialize(null, null, null, onClickCancel);
    }
}
