using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    /// <summary> ゲームスタート </summary>
    Start,
    /// <summary> 設置するオブジェクトを選ぶ </summary>
    SelectObject,
    /// <summary> Objectの設置位置を決定する </summary>
    Prepare,
    /// <summary> ゲームが始まる前のカウントダウン </summary>
    CountDownPlaying,
    /// <summary> ゲームプレイ </summary>
    Playing,
    /// <summary> ゲーム終了 </summary>
    GameClear
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    /// <summary> 現在のGameState </summary>
    private GameState m_nowGameState;
    /// <summary> 現在のGameState </summary>
    public GameState NowGameState => m_nowGameState;

    /// <summary> Playerのオブジェクト </summary>
    GameObject m_player;
    /// <summary> 準備フェーズのカメラ </summary>
    [SerializeField] Camera m_prepareCamera = null;

    void Awake()
    {
        Instance = this;
        SetNowState(GameState.Start); // 初期化
        m_player = Resources.Load<GameObject>("Player/" + SelectCharaInfo.CharaName);
    }

    /// <summary>
    /// GameStateを変更する
    /// </summary>
    /// <param name="state"></param>
    public void SetNowState(GameState state)
    {
        m_nowGameState = state;
        OnGameStateChanged(m_nowGameState);
    }
    
    /// <summary>
    /// GameStateが変更されたときの処理
    /// </summary>
    /// <param name="state"></param>
    void OnGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                Debug.Log("GameState.Start");
                OnStartState();
                break;
            case GameState.SelectObject:
                Debug.Log("GameState.SelectObject");
                OnSelectObjectState();
                break;
            case GameState.Prepare:
                Debug.Log("GameState.PrepareObject");
                OnPrepareState();
                break;
            case GameState.CountDownPlaying:
                Debug.Log("GameState.CountDownPlaying");
                OnCountDownState();
                break;
            case GameState.Playing:
                Debug.Log("GameState.Playing");
                break;
            case GameState.GameClear:
                Debug.Log("GameState.GameClear");
                OnGameClearState();
                break;
            default:
                break;
        }
    }

    /// <summary> GameStartTextのオブジェクト </summary>
    [SerializeField] GameObject m_gameStartTextObj = null;
    /// <summary> 表示する秒数 </summary>
    float m_indicateTime = 2f;
    
    /// <summary> GameStateがStartになったときの処理 </summary>
    void OnStartState()
    {
        StartCoroutine(StartUIAnimation());
        CameraSetting(m_prepareCamera, true);
    }

    /// <summary>
    /// GameStartのUIを指定した秒数だけ表示する
    /// </summary>
    /// <returns></returns>
    IEnumerator StartUIAnimation()
    {
        m_gameStartTextObj.transform.localScale = Vector3.one;
        yield return new WaitForSeconds(m_indicateTime);
        m_gameStartTextObj.transform.localScale = Vector3.zero;
        SetNowState(GameState.SelectObject); // 選択フェーズに移行します
        yield break;
    }

    /// <summary> オブジェクトを選択するためのPanel </summary>
    [Header("Obj選択パネル"),SerializeField] GameObject m_selectPanel = null;

    /// <summary> GameStateがObjectSelectになったときの処理 </summary>
    void OnSelectObjectState()
    {
        m_selectPanel.SetActive(true);
    }


    /// <summary> FixedFieldManager </summary>
    FixedFieldManager m_fixedFieldManager;

    /// <summary> GameStateがPrepareになったときの処理 </summary>
    void OnPrepareState()
    {
        m_fixedFieldManager = FindObjectOfType<FixedFieldManager>().GetComponent<FixedFieldManager>();
        Instantiate(m_fixedFieldManager.SelectedFixedField(), Vector3.zero, Quaternion.identity);
    }


    /// <summary> Joystick </summary>
    [Header("JoyStick"), SerializeField] GameObject m_joystick = null;
    /// <summary> Canvas </summary>
    [SerializeField] GameObject m_canvas = null;
    /// <summary> 生成するJumpButton </summary>
    [Header("Jumpボタン"), SerializeField] GameObject m_jumpButton = null;

    [SerializeField] Transform m_spawnPos = null;

    /// <summary> GameStateがCountDownPlayingになったときの処理 </summary>
    void OnCountDownState()
    {
        Instantiate(m_player, m_spawnPos.position, Quaternion.identity);
        m_joystick.SetActive(true);
        CameraSetting(m_prepareCamera, false);
        GameObject jumpButton = Instantiate(m_jumpButton) as GameObject;
        jumpButton.transform.SetParent(m_canvas.transform, false);
        StartCoroutine(CountDown());
    }

    /// <summary> カウント </summary>
    int m_count = 3;
    /// <summary> Countを表示するテキストのオブジェクト </summary>
    [SerializeField] GameObject m_countTextObj = null;
    /// <summary> Countを表示するテキスト </summary>
    Text m_countText;

    /// <summary>
    /// カウントダウンをするコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator CountDown()
    {
        m_countText = m_countTextObj.GetComponent<Text>();
        m_countTextObj.SetActive(true);
        // カウントの数だけループさせる
        while (m_count > 0)
        {
            m_countText.text = m_count.ToString();
            yield return new WaitForSeconds(1f);
            m_count--;
        }
        m_countTextObj.SetActive(false);
        SetNowState(GameState.Playing);
    }

    /// <summary>
    /// カメラの有効、無効を設定する
    /// </summary>
    /// <param name="camera"> Camera </param>
    /// <param name="enable"> true:有効、false:無効</param>
    void CameraSetting(Camera camera, bool enable)
    {
        camera.enabled = enable;
    }

    /// <summary> GameClearを表示するテキスト </summary>
    [SerializeField] GameObject m_gameClearText = null;

    /// <summary>
    /// GameState.GameClearになったときの処理
    /// </summary>
    void OnGameClearState()
    {
        m_gameClearText.transform.localScale = Vector3.one;
    }
}
