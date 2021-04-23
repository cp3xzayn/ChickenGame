using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// PCでもスマホでも使用できる1人称視点のカメラ移動（スマホは視点移動＋プレイヤーの移動）
/// </summary>
public class SwipeController : MonoBehaviour
{
    /// <summary> スマホをSwipeしたときの回転スピード </summary>
    [SerializeField] float m_swipeTurnSpeed = 0.1f;
    /// <summary> 視点移動のモード </summary>
    [SerializeField] SwipeMode m_swipeMode = SwipeMode.mouse;
    /// <summary> Playerの移動速度 </summary>
    float m_moveSpeed = 2f;

    float m_jumpForce = 500f;
    Rigidbody m_rb;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        //m_jumpButton = GameObject.FindWithTag("JumpButton").GetComponent<Button>();
        //m_jumpButton.onClick.AddListener(() => OnClickJump());
    }

    void Update()
    {
        switch (m_swipeMode)
        {
            case SwipeMode.mouse:
                MouseSwipe();
                break;
            case SwipeMode.phone:
                TouchSwipe();
                break;
        }
    }


    // ↓スマホでの視点操作+Playerの移動

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

    /// <summary> 最初にタッチされた座標 </summary>
    Vector3 startTouchPos;

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
            // それぞれの角度をセットする
            //今回はY方向の視点移動を使用しないため、xも0になっている
            this.transform.eulerAngles = new Vector3(0, angleY, 0);
        }
        if (touch.position.x < Screen.width / 2)
        {
            // タッチされたとき
            if (touch.phase == TouchPhase.Began)
            {
                // 初期化する
                startTouchPos = touch.position;
            }
            // タッチが継続的に続いているとき
            if (touch.phase == TouchPhase.Stationary)
            {
                Vector3 nowTouchPos = touch.position; // 現在のタッチしている座標
                // 差分を単位ベクトルにする
                Vector3 diffPosNormalized = (nowTouchPos - startTouchPos).normalized; 
                // プレイヤーを移動する
                m_rb.velocity = transform.forward * diffPosNormalized.y * m_moveSpeed;
            }

        }
    }

    //Button m_jumpButton;

    //public void OnClickJump()
    //{
    //    Debug.Log("a");
    //    m_rb.AddForce(transform.up * m_jumpForce, ForceMode.Impulse);
    //}


    // ↓マウスでの操作（視点操作のみ）
    
    /// <summary> マウスをSwipeしたときの回転スピード </summary>
    [SerializeField] float m_mouseTurnSpeed = 0.01f;
    /// <summary> タッチされた初期位置 </summary>
    Vector3 m_mousePos;
    /// <summary> 画面の右側が選択されたかを判定する </summary>
    bool isRightOnScreen = false;

    /// <summary>
    /// マウスでのスワイプに関する処理
    /// </summary>
    void MouseSwipe()
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
                float angleX = this.transform.eulerAngles.x + diffMousePos.y * m_mouseTurnSpeed;

                // 左右視点の角度
                float angleY = this.transform.eulerAngles.y + diffMousePos.x * m_mouseTurnSpeed;

                // それぞれの角度をセットする
                this.transform.eulerAngles = new Vector3(0, angleY, 0); //今回はY方向の視点移動を使用しないため、xも0になっている
            }
        }
    }
}

/// <summary>
/// 視点移動の操作モード(PC, スマホ)
/// </summary>
public enum SwipeMode
{
    mouse,
    phone
}
