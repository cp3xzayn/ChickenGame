using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerSettingManager : MonoBehaviour
{
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
        m_audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 感度を設定する
    /// </summary>
    public void SetSensitivity()
    {
        PlayerSetting.XSensitivity = m_hSensitivitySlider.value;
        PlayerSetting.YSensitivity = m_vSensitivitySlider.value;
    }

    /// <summary>
    /// BGMの音量を設定する
    /// </summary>
    public void SetBGMVolume()
    {
        PlayerSetting.BGMVolume = m_bGMVolumeSlider.value;
        m_audioSource.volume = PlayerSetting.BGMVolume;
    }
}
