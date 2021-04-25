using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetting
{
    /// <summary> 水平方向の感度 </summary>
    private static float m_xSensitivity;
    /// <summary> 垂直方向の感度 </summary>
    private static float m_ySensitivity;

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
}
