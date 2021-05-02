using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CharaData", menuName = "ScriptableObjects/CreateCharaDataAsset")]
public class CharacterSelectData : ScriptableObject
{
    public List<CharacterParam> m_charaParamList = new List<CharacterParam>();
}

[System.Serializable]
public class CharacterParam
{
    /// <summary> キャラの名前 </summary>
    [Header("キャラの名前")] public string m_name;
    /// <summary> キャラの画像 </summary>
    [Header("キャラの画像")] public Sprite m_image;
}

