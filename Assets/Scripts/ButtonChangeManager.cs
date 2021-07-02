using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.IO;

/// <summary>
/// Buttonのデータの反映、処理を行うクラス
/// </summary>
public class ButtonChangeManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /// <summary> ButtonDataのファイル名 </summary>
    static string m_buttonFileName = "ButtonData";

    /// <summary> ゲーム起動後最初に呼び出す </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void InitializeBeforeSceneLoad()
    {
        if (!File.Exists(FileManager.GetFilePath(m_buttonFileName)))
        {
            ButtonData buttonData = new ButtonData();
            FileManager.TextSave(m_buttonFileName, JsonUtility.ToJson(buttonData));
        }
    }

    /// <summary>
    /// ButtonDataをロードし返す
    /// </summary>
    public ButtonData LoadButtonSetting
    {
        get
        {
            ButtonData buttonData = JsonUtility.FromJson<ButtonData>(FileManager.TextLoad("ButtonData"));
            return buttonData;
        }
    }

    // 今後修正必要
    /// <summary> Buttonの配列 </summary>
    [SerializeField] GameObject[] m_buttons = null;
    /// <summary> 選択されたButtonのGameObject </summary>
    GameObject m_selectedButton = null;
    /// <summary> 選択されたボタンのRectTransform </summary>
    RectTransform m_rtSelectedButton;
    /// <summary> 移動するButtonのRectTransform </summary>
    [SerializeField] RectTransform m_rtMoveButton = null;


    /// <summary> 2本目の指が押されたときの距離 </summary>
    float m_backDistance;

    /// <summary> Buttonの初期Xサイズ </summary>
    float m_defaultScaleX;
    /// <summary> Buttonの初期Yサイズ </summary>
    float m_defaultScaleY;

    /// <summary> 変更後のXサイズ </summary>
    float m_changedScaleX;
    /// <summary> 変更後のYサイズ </summary>
    float m_changedScaleY;

    void Start()
    {
        // 保存されたデータを取得し、前回設定したButtonの設定を反映する
        m_rtMoveButton.localPosition = LoadButtonSetting.m_buttonPos;
        m_rtMoveButton.sizeDelta = LoadButtonSetting.m_buttonSize;

        for (int i = 0; i < m_buttons.Length; i++)
        {
            int value = i; //キャプチャーのため、変数に格納する
            m_buttons[i].GetComponent<Button>().onClick.AddListener(() => OnClickButton(value));
        }
    }

    void Update()
    {
        if (Application.isEditor)
        {
        }
        else
        {
            GetTouchDistance();
        }

    }

    /// <summary>
    /// Buttonが押されたときの処理
    /// </summary>
    /// <param name="value"></param>
    public void OnClickButton(int value)
    {
        Debug.Log("Button選択");
        m_selectedButton = m_buttons[value];
        m_rtSelectedButton = m_selectedButton.GetComponent<RectTransform>();
        m_defaultScaleX = m_rtSelectedButton.sizeDelta.x;
        m_defaultScaleY = m_rtSelectedButton.sizeDelta.y;
    }

    /// <summary>
    /// 2本指でタッチされたときの距離を取得し、ButtonSizeを変更する(スマホ)
    /// </summary>
    void GetTouchDistance()
    {
        if (Input.touchCount >= 2)
        {
            //タッチした2点を取得
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            // 2本目の指がタッチされたとき
            if (touch2.phase == TouchPhase.Began)
            {
                //　距離を初期化する
                m_backDistance = Vector2.Distance(touch1.position, touch2.position);
            }
            else if (touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
            {
                // 現在の2点の距離を取得
                float newDistance = Vector2.Distance(touch1.position, touch2.position);
                // 指の距離の偏差分を取り、サイズを変更する(最大サイズ：２，最小サイズ：0.5)
                m_changedScaleX = m_defaultScaleX + (newDistance - m_backDistance) / 2;
                m_changedScaleY = m_defaultScaleY + (newDistance - m_backDistance) / 2;

                if (m_changedScaleX > 0 && m_changedScaleY > 0)
                {
                    UpdateScaling(m_changedScaleX, m_changedScaleY);
                }
            }
        }
    }



    /// <summary>
    /// ButtonのScaleを変更する
    /// </summary>
    /// <param name="xScale"></param>
    /// <param name="yScale"></param>
    void UpdateScaling(float xScale, float yScale)
    {
        m_rtSelectedButton.sizeDelta = new Vector2(xScale, yScale);
    }

    /// <summary>
    /// ドラッグ開始時に呼び出される
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Application.isEditor)
        {
            m_selectedButton.transform.position = transform.position;
        }
        else
        {
            if (Input.touchCount == 1)
            {
                m_selectedButton.transform.position = transform.position;
            }
        }
    }

    /// <summary>
    /// ドラッグ中に呼び出される
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (Application.isEditor)
        {
            m_selectedButton.transform.position = eventData.position;
        }
        else
        {
            if (Input.touchCount == 1)
            {
                m_selectedButton.transform.position = eventData.position;
            }
        }
    }

    /// <summary>
    /// ドラッグ終了時に呼ばれる処理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        SetButtonData();
    }


    /// <summary>
    /// ButtonSetting画面が閉じられた時
    /// </summary>
    public void OnCloseButtonSetting()
    {
        SaveButtonData(ButtonSetting.m_buttonData);
    }

    /// <summary>
    /// ButtonSettingをセーブする
    /// </summary>
    /// <param name="buttonData"></param>
    public void SaveButtonData(ButtonData buttonData)
    {
        Debug.Log("ファイルにButtonDataを保存しました。");
        FileManager.TextSave(m_buttonFileName, JsonUtility.ToJson(buttonData));
    }

    /// <summary>
    /// ButtonSettingにButtonの情報をセットする
    /// </summary>
    void SetButtonData()
    {
        Debug.Log("ButtonSettingにデータをセットしました。");
        
        ButtonSetting.ButtonPos = new Vector2(m_rtSelectedButton.localPosition.x, m_rtSelectedButton.localPosition.y);
        ButtonSetting.ButtonSize = new Vector2(m_rtSelectedButton.sizeDelta.x, m_rtSelectedButton.sizeDelta.y);
        Debug.Log("setbuttondata" + ButtonSetting.ButtonPos);
    }

}

/// <summary>
/// Buttonのポジション、サイズの設定データ
/// </summary>
[Serializable]
public class ButtonData
{
    public Vector2 m_buttonPos = new Vector2(650, -210);
    public Vector2 m_buttonSize = new Vector2(150, 150);
}
