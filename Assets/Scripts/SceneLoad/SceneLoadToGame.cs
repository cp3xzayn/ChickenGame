using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadToGame : MonoBehaviour
{
    /// <summary>
    /// ゲームスタートボタンが押されたときの処理
    /// </summary>
    public void OnClickStart()
    {
        FadeManager.Instance.LoadScene("WaitingPlayerScene", 1.0f);
    }
}
