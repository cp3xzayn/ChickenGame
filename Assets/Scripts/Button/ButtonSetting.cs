using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ButtonDataを保持するクラス
/// </summary>
public class ButtonSetting
{
    /// <summary>
    /// ButtonDataのインスタンス
    /// </summary>
    public static ButtonData m_buttonData = new ButtonData();

    /// <summary> Buttonのポジション </summary>
    public static Vector2 ButtonPos
    {
        set { m_buttonData.m_buttonPos = value; }
        get { return m_buttonData.m_buttonPos; }
    }

    /// <summary> Buttonのサイズ </summary>
    public static Vector2 ButtonSize
    {
        set { m_buttonData.m_buttonSize = value; }
        get { return m_buttonData.m_buttonSize; }
    }
}
