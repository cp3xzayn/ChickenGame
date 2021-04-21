using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PCでもスマホでも使用できる1人称視点のカメラ移動
/// </summary>
public class SwipeController : MonoBehaviour
{
    /// <summary> Swipeしたときの回転スピード </summary>
    [SerializeField] float m_swipeTurnSpeed = 0.01f;

    /// <summary> タッチされた初期位置 </summary>
    Vector3 m_mousePos;
    /// <summary> 画面の右側が選択されたかを判定する </summary>
    bool isRightOnScreen = false;

    void Update()
    {
        // マウスが押されたタイミングで
        if (Input.GetMouseButtonDown(0))
        {
            // 初期位置を初期化する
            m_mousePos = Input.mousePosition;
            if (m_mousePos.x >= Screen.width / 2)
            {
                isRightOnScreen = true;
            }
            else
            {
                isRightOnScreen = false;
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (isRightOnScreen)
            {
                Vector3 nextMousePos = Input.mousePosition;
                Vector3 diffMousePos = m_mousePos - nextMousePos;

                // 上下視点の角度(制限をつける)
                float angleX = this.transform.eulerAngles.x + diffMousePos.y * m_swipeTurnSpeed;

                // 左右視点の角度
                float angleY = this.transform.eulerAngles.y + diffMousePos.x * m_swipeTurnSpeed;

                // それぞれの角度をセットする
                this.transform.eulerAngles = new Vector3(0, angleY, 0); //今回はY方向の視点移動を使用しないため、xも0になっている
            }
        }
    }
}
