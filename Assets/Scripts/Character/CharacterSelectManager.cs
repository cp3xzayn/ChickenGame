using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectManager : MonoBehaviour
{
    /// <summary> CharacterSelectDataのScriptableObject </summary>
    [SerializeField] CharacterSelectData m_charaSelectData = null;
    /// <summary> キャラの名前のText </summary>
    [SerializeField] Text[] m_nameText = null;
    /// <summary> キャラの画像 </summary>
    [SerializeField] Image[] m_charaImage = null;


    /// <summary> キャラ選択Panel </summary>
    [SerializeField] GameObject m_charaPanel = null;
    /// <summary> 選ばれたキャラを生成する場所 </summary>
    [SerializeField] Transform m_generateTransform = null;

    /// <summary> 選択されたキャラの名前 </summary>
    private string m_selectCharaName;
    /// <summary> 選択されたキャラの名前 </summary>
    public string SelectCharaName => m_selectCharaName;

    CharaSelectPhase m_nowPhase;


    void Awake()
    {
        SetNowPhase(CharaSelectPhase.Initialize);
    }

    /// <summary>
    /// このメソッドを用いてPhaseを変更する
    /// </summary>
    /// <param name="phase"></param>
    void SetNowPhase(CharaSelectPhase phase)
    {
        m_nowPhase = phase;
        OnChangePhase(m_nowPhase);
    }

    /// <summary>
    /// Phaseが変わったら何をするか
    /// </summary>
    /// <param name="phase"></param>
    void OnChangePhase(CharaSelectPhase phase)
    {
        switch (phase)
        {
            case CharaSelectPhase.Initialize:
                CharaDataSet();
                break;
            case CharaSelectPhase.Selected:
                OnSelectedPhase();
                break;
        }
    }

    /// <summary>
    /// キャラの情報をセットする
    /// </summary>
    void CharaDataSet()
    {
        for (int i = 0; i < m_charaSelectData.m_charaParamList.Count; i++)
        {
            m_nameText[i].text = m_charaSelectData.m_charaParamList[i].m_name;
            m_charaImage[i].sprite = m_charaSelectData.m_charaParamList[i].m_image;
        }
    }

    /// <summary>
    /// どのボタンが押されたかnameで判断する
    /// </summary>
    /// <param name="name"></param>
    public void OnClickCharaSelect(string name)
    {
        m_selectCharaName = name;
        SelectCharaInfo.CharaName = m_selectCharaName;
        SetNowPhase(CharaSelectPhase.Selected);
    }

    void OnSelectedPhase()
    {
        m_charaPanel.SetActive(false);
        Instantiate(Resources.Load<GameObject>("Character/" + m_selectCharaName),
            m_generateTransform.position, Quaternion.identity);
    }

    public void OnClickToGame()
    {
        FadeManager.Instance.LoadScene("GameScene", 1.0f);
    }
}


public enum CharaSelectPhase
{
    Initialize,
    Selected
}
