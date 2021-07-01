using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

/// <summary>
/// 感度や音量などの設定を管理するクラス
/// </summary>
public class PlayerSettingManager : MonoBehaviour
{
    /// <summary>SettingDataのファイル名</summary>
    static string m_settingFileName = "SettingData";
    /// <summary> ButtonDataのファイル名 </summary>
    static string m_buttonFileName = "ButtonData";

    /// <summary> ゲーム起動後最初に呼び出す </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void InitializeBeforeSceneLoad()
    {
        // 設定画面のCanvasをロードし生成する(シーンをまたいでも消えないようにする)
        var settingCanvas = GameObject.Instantiate(Resources.Load("SettingCanvas"));
        GameObject.DontDestroyOnLoad(settingCanvas);
        //データがなかったら作成する
        if (!File.Exists(FileManager.GetFilePath(m_settingFileName)))
        {
            // 設定のデータを初期化しJsonファイルを作成する
            SettingData settingData = new SettingData();
            FileManager.TextSave(m_settingFileName, JsonUtility.ToJson(settingData));
        }
        if (!File.Exists(FileManager.GetFilePath(m_buttonFileName)))
        {
            ButtonData buttonData = new ButtonData();
            FileManager.TextSave(m_buttonFileName, JsonUtility.ToJson(buttonData));
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
        Debug.Log(FileManager.GetFilePath(m_settingFileName));
        
        // Sliedrのvalue、ToggleのOnOffを初期化する
        m_hSensitivitySlider.value = LoadPlayerSetting.m_xSensitivity;
        m_vSensitivitySlider.value = LoadPlayerSetting.m_ySensitivity;
        m_bGMVolumeSlider.value = LoadPlayerSetting.m_bGMVolume;
        m_sEVolumeSlider.value = LoadPlayerSetting.m_sEVolume;
        m_tutorialToggle.isOn = LoadPlayerSetting.isTutorial;
    }


    /// <summary>
    /// 設定データをロードし返す
    /// </summary>
    public SettingData LoadPlayerSetting
    {
        get
        {
            SettingData settingData = JsonUtility.FromJson<SettingData>(FileManager.TextLoad(m_settingFileName));
            return settingData;
        }
    }


    public ButtonData LoadButtonSetting
    {
        get
        {
            ButtonData buttonData = JsonUtility.FromJson<ButtonData>(FileManager.TextLoad(m_buttonFileName));
            return buttonData;
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
    /// ButtonSetting画面が閉じられた時
    /// </summary>
    public void OnClickButtonSetting()
    {
        SaveButtonData(ButtonSetting.m_buttonData);
    }

    /// <summary>
    /// PlayerSettingをセーブする
    /// </summary>
    public void SaveSettingData(SettingData settingData)
    {
        Debug.Log("$ファイルにPlayerSettingを保存しました。");
        FileManager.TextSave(m_settingFileName, JsonUtility.ToJson(settingData));
    }

    /// <summary>
    /// ButtonSettingをセーブする
    /// </summary>
    /// <param name="buttonData"></param>
    public void SaveButtonData(ButtonData buttonData)
    {
        Debug.Log("ファイルにButtonDataを保存しました。");
        FileManager.TextSave(m_buttonFileName, JsonUtility.ToJson(buttonData));
    }
}


/// <summary>
/// 設定データ(Json形式)
/// </summary>
[Serializable]
public class SettingData
{
    /// <summary> 水平方向感度 </summary>
    public float m_xSensitivity = 0.1f;
    /// <summary> 垂直方向感度 </summary>
    public float m_ySensitivity = 0.1f;
    /// <summary> BGMの音量 </summary>
    public float m_bGMVolume = 0.1f;
    /// <summary> 効果音の音量 </summary>
    public float m_sEVolume = 0.1f;
    /// <summary> チュートリアルの説明のOnOffを決めます </summary>
    public bool isTutorial = true;
}


/// <summary>
/// Buttonのポジション、サイズの設定データ
/// </summary>
[Serializable]
public class ButtonData
{
    public Vector2 m_buttonPos = new Vector2(-150, 150);
    public Vector2 m_buttonSize = new Vector2(150, 150);
}
