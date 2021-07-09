using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class ButtonAnimationInterface : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    /// <summary> ButtonのRectTransform </summary>
    RectTransform rt;
    /// <summary> Button縮小時のサイズ </summary>
    [Header("ボタン縮小時のサイズ"), SerializeField] float m_minSize = 0.9f;
    /// <summary> Button拡大時のサイズ </summary>
    [Header("ボタン拡大時のサイズ"), SerializeField] float m_maxSize = 1f;
    /// <summary> ButtonがクリックSound </summary>
    [Header("クリック音"), SerializeField] AudioClip m_clickSound = null;
    /// <summary> AudioSource </summary>
    AudioSource m_audioSource;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        m_audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Button押下時の処理
    /// </summary>
    /// <param name="pointerEventData"></param>
    public virtual void OnPointerDown(PointerEventData pointerEventData)
    {
        Debug.Log("ボタンが押されました。");
        rt.localScale = new Vector2(m_minSize, m_minSize);
        m_audioSource.PlayOneShot(m_clickSound);
    }

    /// <summary>
    /// Buttonから離れたときの処理
    /// </summary>
    /// <param name="eventData"></param>
    public virtual void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("ボタンから離れました。");
        rt.localScale = new Vector2(m_maxSize, m_maxSize);
    }
}
