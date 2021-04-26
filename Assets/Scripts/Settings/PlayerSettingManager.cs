using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class PlayerSettingManager : MonoBehaviour
{
    /// <summary>テキストファイルの名前をSettingとする</summary>
    static string m_textName = "SettingData";

    /// <summary> ゲーム起動後最初に呼び出す </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void InitializeBeforeSceneLoad()
    {
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
            FileManager.TextSave(m_textName, JsonUtility.ToJson(m_settingData));
        }
    }

    /// <summary> 水平感度設定のためのSlider </summary>
    [SerializeField] Slider m_hSensitivitySlider = null;
    /// <summary> 垂直感度設定のためのSlider </summary>
    [SerializeField] Slider m_vSensitivitySlider = null;
    /// <summary> BGM音量調節のためのSlider </summary>
    [SerializeField] Slider m_bGMVolumeSlider = null;
    /// <summary> AudioSource </summary>
    AudioSource m_audioSource;

    void Start()
    {
        SettingData m_settingData = new SettingData();
        m_audioSource = GetComponent<AudioSource>();
        Debug.Log(FileManager.GetFilePath(m_textName));
        // Sliedrのvalueを初期化する
        m_hSensitivitySlider.value = m_settingData.m_xSensitivity;
        m_vSensitivitySlider.value = m_settingData.m_ySensitivity;
        m_bGMVolumeSlider.value = m_settingData.m_bGMVolume;
    }


    /// <summary>
    /// 設定データをロードし返す
    /// </summary>
    public SettingData LoadSetting
    {
        get
        {
            SettingData settingData = JsonUtility.FromJson<SettingData>(FileManager.GetFilePath(m_textName));
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
    public float m_xSensitivity;
    public float m_ySensitivity;
    public float m_bGMVolume;
}
