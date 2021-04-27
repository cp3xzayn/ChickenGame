using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playerが設定した設定を保持するクラス
/// </summary>
public class PlayerSetting
{
    /// <summary>
    /// 設定のデータ(Json形式)のインスタンス
    /// </summary>
    public static SettingData m_settingData = new SettingData();

    /// <summary> 水平方向の感度 </summary>
    public static float XSensitivity
    {
        set { m_settingData.m_xSensitivity = value; }
        get { return m_settingData.m_xSensitivity; }
    }
    /// <summary> 垂直方向の感度 </summary>
    public static float YSensitivity
    {
        set { m_settingData.m_ySensitivity = value; }
        get { return m_settingData.m_ySensitivity; }
    }

    /// <summary> BGMの音量 </summary>
    public static float BGMVolume
    {
        set { m_settingData.m_bGMVolume = value; }
        get { return m_settingData.m_bGMVolume; }
    }

    /// <summary> 効果音の音量 </summary>
    public static float SEVolume
    {
        set { m_settingData.m_sEVolume = value; }
        get { return m_settingData.m_sEVolume; }
    }

    /// <summary> TutorialのOnOffを決める </summary>
    public static bool IsTutorial
    {
        set { m_settingData.isTutorial = value; }
        get { return m_settingData.isTutorial; }
    }
}
