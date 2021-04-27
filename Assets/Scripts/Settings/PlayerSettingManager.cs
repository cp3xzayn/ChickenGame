using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

/// <summary>
/// 感度や音量などの設定を管理するクラス
/// </summary>
public class PlayerSettingManager : MonoBehaviour
{
    /// <summary>テキストファイルの名前をSettingとする</summary>
    static string m_textName = "SettingData";

    /// <summary> ゲーム起動後最初に呼び出す </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void InitializeBeforeSceneLoad()
    {
        // 設定画面のCanvasをロードし生成する(シーンをまたいでも消えないようにする)
        var settingCanvas = GameObject.Instantiate(Resources.Load("SettingCanvas"));
        GameObject.DontDestroyOnLoad(settingCanvas);
        //データがなかったら作成する
        if (!File.Exists(FileManager.GetFilePath(m_textName)))
        {
            // 設定のデータを初期化しJsonファイルを作成する
            SettingData m_settingData = new SettingData();
            m_settingData.m_xSensitivity = 0.1f;
            m_settingData.m_ySensitivity = 0.1f;
            m_settingData.m_bGMVolume = 0.1f;
            m_settingData.m_sEVolume = 0.1f;
            m_settingData.isTutorial = true;
            FileManager.TextSave(m_textName, JsonUtility.ToJson(m_settingData));
        }
    }

    /// <summary> 水平感度設定のためのSlider </summary>
    [SerializeField] Slider m_hSensitivitySlider = null;
    /// <summary> 垂直感度設定のためのSlider </summary>
    [SerializeField] Slider m_vSensitivitySlider = null;
    /// <summary> BGM音量調節のためのSlider </summary>
    [SerializeField] Slider m_bGMVolumeSlider = null;
    /// <summary> 効果音調節のためのSlider </summary>
    [SerializeField] Slider m_sEVolumeSlider = null;
    /// <summary> TutorialのOnOffを設定するToggle </summary>
    [SerializeField] Toggle m_tutorialToggle = null;
    /// <summary> AudioSource </summary>
    [SerializeField] AudioSource m_audioSource = null;

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        Debug.Log(FileManager.GetFilePath(m_textName));
        
        // Sliedrのvalue、ToggleのOnOffを初期化する
        m_hSensitivitySlider.value = LoadSetting.m_xSensitivity;
        m_vSensitivitySlider.value = LoadSetting.m_ySensitivity;
        m_bGMVolumeSlider.value = LoadSetting.m_bGMVolume;
        m_sEVolumeSlider.value = LoadSetting.m_sEVolume;
        m_tutorialToggle.isOn = LoadSetting.isTutorial;
    }


    /// <summary>
    /// 設定データをロードし返す
    /// </summary>
    public SettingData LoadSetting
    {
        get
        {
            SettingData settingData = JsonUtility.FromJson<SettingData>(FileManager.TextLoad(m_textName));
            return settingData;
        }
    }

    /// <summary>
    /// 感度を設定する(Sliderが変更されたとき)
    /// </summary>
    public void SetSensitivity()
    {
        PlayerSetting.XSensitivity = m_hSensitivitySlider.value;
        PlayerSetting.YSensitivity = m_vSensitivitySlider.value;
    }

    /// <summary>
    /// BGMの音量を設定する(Sliderが変更されたとき)
    /// </summary>
    public void SetBGMVolume()
    {
        PlayerSetting.BGMVolume = m_bGMVolumeSlider.value;
        m_audioSource.volume = PlayerSetting.BGMVolume;
    }

    /// <summary>
    /// SEの音量を設定する(Sliderが変更されたとき)
    /// </summary>
    public void SetSEVolume()
    {
        PlayerSetting.SEVolume = m_sEVolumeSlider.value;
    }

    /// <summary>
    /// TutorialのOnOffを保存する
    /// </summary>
    public void TutorialOnOff()
    {
        PlayerSetting.IsTutorial = m_tutorialToggle.isOn;
    }

    /// <summary>
    /// 設定画面が閉じられた時
    /// </summary>
    public void OnClickCloseSetting()
    {
        SaveSettingData(PlayerSetting.m_settingData);
    }

    /// <summary>
    /// 設定をセーブする
    /// </summary>
    public void SaveSettingData(SettingData settingData)
    {
        Debug.Log("$ファイルに設定データを保存しました。");
        FileManager.TextSave(m_textName, JsonUtility.ToJson(settingData));
    }
}


/// <summary>
/// 設定データ(Json形式)
/// </summary>
[Serializable]
public class SettingData
{
    /// <summary> 水平方向感度 </summary>
    public float m_xSensitivity;
    /// <summary> 垂直方向感度 </summary>
    public float m_ySensitivity;
    /// <summary> BGMの音量 </summary>
    public float m_bGMVolume;
    /// <summary> 効果音の音量 </summary>
    public float m_sEVolume;
    /// <summary> チュートリアルの説明のOnOffを決めます </summary>
    public bool isTutorial;
}
