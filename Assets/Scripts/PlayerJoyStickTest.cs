using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoyStickTest : MonoBehaviour
{
    /// <summary> Playerの初期ポジション </summary>
    Vector3 m_startPos;
    /// <summary> RigidBody </summary>
    Rigidbody m_rb;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_startPos = this.transform.position;
    }

    void Update()
    {
        TouchSwipe();
    }

    /// <summary> スマホをSwipeしたときの回転スピード </summary>
    [Header("スマホでの視点回転スピード"), SerializeField] float m_swipeTurnSpeed = 0.1f;
    /// <summary> Playerの移動速度 </summary>
    [Header("スマホでの移動速度"), SerializeField] float m_moveSpeed = 5f;
    /// <summary> 最初にタッチされた座標 </summary>
    Vector3 startTouchPos;

    [SerializeField] FixedJoystick m_fixedJoystick = null;

    /// <summary>
    /// スマホ画面のタッチでの視点移動+Playerの移動
    /// </summary>
    void TouchSwipe()
    {
        Touch touch;
        // タッチされた指の数
        int touchCount = Input.touchCount;

        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);
            BehaviorFromTouch(touch);
        }
        if (Input.touchCount == 2)
        {
            for (int i = 0; i < touchCount; i++)
            {
                touch = Input.GetTouch(i);
                BehaviorFromTouch(touch);
            }
        }
    }

    /// <summary>
    /// タッチされた場所が画面を半分にしたときの右か左かを判定する
    /// 左：プレイヤーの移動　右：視点移動
    /// </summary>
    /// <param name="touch"></param>
    void BehaviorFromTouch(Touch touch)
    {
        if (touch.position.x >= Screen.width / 2)
        {
            float x = touch.deltaPosition.x; // 偏差分を求める
            // 左右に視点変更する時の角度
            float angleY = this.transform.eulerAngles.y + x * m_swipeTurnSpeed;
            // 移動する角度をセットする
            this.transform.eulerAngles = new Vector3(0, angleY, 0);
        }
        if (touch.position.x < Screen.width / 2)
        {
            float h = m_fixedJoystick.Horizontal;
            float v = m_fixedJoystick.Vertical;

            Vector3 dir = transform.forward * v + transform.right * h;
            m_rb.velocity = dir * m_moveSpeed;
        }
    }
}
