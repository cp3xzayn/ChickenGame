using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeforeLoadScene
{
    /// <summary> ゲーム起動後最初に呼び出す </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void InitializeBeforeSceneLoad()
    {
        var settingCanvas = GameObject.Instantiate(Resources.Load("SettingCanvas"));
        GameObject.DontDestroyOnLoad(settingCanvas);
    }
}
