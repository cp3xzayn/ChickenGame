using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playerが設定した設定を保持するクラス
/// </summary>
public class PlayerSetting
{
    /// <summary> 水平方向の感度 </summary>
    private static float m_xSensitivity = 0.1f;
    /// <summary> 垂直方向の感度 </summary>
    private static float m_ySensitivity = 0.1f;

    /// <summary> 水平方向の感度 </summary>
    public static float XSensitivity
    {
        set { m_xSensitivity = value; }
        get { return m_xSensitivity; }
    }
    /// <summary> 垂直方向の感度 </summary>
    public static float YSensitivity
    {
        set { m_ySensitivity = value; }
        get { return m_ySensitivity; }
    }

    /// <summary> BGMの音量 </summary>
    private static float m_bGMVolume = 0.1f;
    /// <summary> BGMの音量 </summary>
    public static float BGMVolume
    {
        set { m_bGMVolume = value; }
        get { return m_bGMVolume; }
    }
}
