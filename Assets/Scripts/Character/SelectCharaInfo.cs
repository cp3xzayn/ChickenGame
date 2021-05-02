using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharaInfo
{
    static string m_charaName;

    public static string CharaName
    {
        set { m_charaName = value; }
        get { return m_charaName; }
    }
}
