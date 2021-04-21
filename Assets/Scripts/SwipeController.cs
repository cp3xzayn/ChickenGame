using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PCでもスマホでも使用できる3人称視点のカメラ移動
/// </summary>
public class SwipeController : MonoBehaviour
{
    /// <summary> Swipeしたときの回転スピード </summary>
    [SerializeField] float m_swipeTurnSpeed = 30f;
    /// <summary> タッチされた初期位置 </summary>
    Vector3 m_mousePos;

    void Update()
    {
        // マウスが押されたタイミングで
        if (Input.GetMouseButtonDown(0))
        {
            // 初期位置を初期化する
            m_mousePos = Input.mousePosition; 
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 nextMousePos = Input.mousePosition;
            Vector3 diffMousePos = m_mousePos - nextMousePos;

            float angleX = this.transform.eulerAngles.x - diffMousePos.y * m_swipeTurnSpeed * 0.01f;
            float angleY = this.transform.eulerAngles.y + diffMousePos.x * m_swipeTurnSpeed * 0.01f;

            this.transform.eulerAngles = new Vector3(angleX, angleY, 0);
        }
    }
}
