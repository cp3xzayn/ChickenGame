using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    /// <summary> ゲームスタート </summary>
    Start,
    /// <summary> 設置するオブジェクトを選ぶ </summary>
    SelectObject,
    /// <summary> Objectの設置位置を決定する </summary>
    Prepare,
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

    /// <summary> 準備フェーズのカメラ </summary>
    [SerializeField] Camera m_prepareCamera = null;
    /// <summary> Playerの視点の噛めた </summary>
    [SerializeField] Camera m_playerCamera = null;

    void Awake()
    {
        Instance = this;
        SetNowState(GameState.Start); // 初期化
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
            case GameState.Playing:
                Debug.Log("GameState.Playing");
                OnPlayingState();
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
        StartCoroutine("StartUIAnimation");
        CameraSetting(m_prepareCamera, true);
        CameraSetting(m_playerCamera, false);
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
        SetNowState(GameState.SelectObject);
        yield break;
    }

    /// <summary> オブジェクトを選択するためのPanel </summary>
    [Header("Obj選択パネル"),SerializeField] GameObject m_selectPanel = null;

    /// <summary> GameStateがObjectSelectになったときの処理 </summary>
    void OnSelectObjectState()
    {
        m_selectPanel.SetActive(true);
    }


    /// <summary> 固定のオブジェクト </summary>
    [Header("固定オブジェクト"), SerializeField] GameObject m_fixedObjects = null;

    /// <summary> GameStateがPrepareになったときの処理 </summary>
    void OnPrepareState()
    {
        m_fixedObjects.SetActive(true);
    }


    /// <summary> Playerのオブジェクト </summary>
    [Header("Player"), SerializeField] GameObject m_player = null;
    /// <summary> Joystick </summary>
    [Header("JoyStick"), SerializeField] GameObject m_joystick = null;
    /// <summary> Canvas </summary>
    [SerializeField] GameObject m_canvas = null;
    /// <summary> 生成するJumpButton </summary>
    [SerializeField] GameObject m_jumpButton = null;

    /// <summary> GameStateがPlayingになったときの処理 </summary>
    void OnPlayingState()
    {
        m_player.SetActive(true);
        m_joystick.SetActive(true);
        CameraSetting(m_prepareCamera, false);
        CameraSetting(m_playerCamera, true);
        GameObject jumpButton = Instantiate(m_jumpButton) as GameObject;
        jumpButton.transform.SetParent(m_canvas.transform, false);
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
