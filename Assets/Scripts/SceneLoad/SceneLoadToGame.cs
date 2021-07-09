using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SceneLoadToGame : ButtonAnimationInterface
{
    /// <summary>
    /// ゲームスタートが押されたときの処理
    /// </summary>
    /// <param name="pointerEventData"></param>
    public override void OnPointerDown(PointerEventData pointerEventData)
    {
        base.OnPointerDown(pointerEventData);
        FadeManager.Instance.LoadScene("WaitingPlayerScene", 1.0f);
    }
}
