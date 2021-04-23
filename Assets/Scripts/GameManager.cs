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
    End
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    /// <summary> 現在のGameState </summary>
    private GameState m_nowGameState;
    /// <summary> 現在のGameState </summary>
    public GameState NowGameState => m_nowGameState;

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
                break;
            case GameState.Playing:
                break;
            case GameState.End:
                break;
            default:
                break;
        }
    }

    /// <summary> GameStartTextのオブジェクト </summary>
    [SerializeField] GameObject m_gameStartTextObj = null;
    /// <summary> 表示する秒数 </summary>
    float m_indicateTime = 2f;
    
    /// <summary>
    /// GameStateがStartになったときの処理
    /// </summary>
    void OnStartState()
    {
        StartCoroutine("StartUIAnimation");
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
    [SerializeField] GameObject m_selectPanel = null;

    /// <summary>
    /// GameStateがObjectSelectになったときの処理
    /// </summary>
    void OnSelectObjectState()
    {
        m_selectPanel.SetActive(true);
    }
}
