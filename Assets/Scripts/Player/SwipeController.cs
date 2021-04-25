using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// PCでもスマホでも使用できる1人称視点のカメラ移動（スマホは視点移動＋プレイヤーの移動）
/// </summary>
public class SwipeController : MonoBehaviour
{
    /// <summary> Playerの初期ポジション </summary>
    Vector3 m_startPos;
    /// <summary> RigidBody </summary>
    Rigidbody m_rb;
    /// <summary> PlayerCameraのゲームオブジェクト </summary>
    [SerializeField] GameObject m_playerCamera = null;
    /// <summary> PlayerCameraY </summary>
    PlayerCameraY m_playerCameraY;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_startPos = this.transform.position;
        m_playerCameraY = m_playerCamera.GetComponent<PlayerCameraY>();
        m_jumpButton = GameObject.FindWithTag("JumpButton");
        Button m_jump = m_jumpButton.GetComponent<Button>();
        m_jump.onClick.AddListener(() => Jump());
    }

    void Update()
    {
        // editor上かスマホかで挙動を変える
        if (Application.isEditor)
        {
            PlayerMoveOnEditor();
        }
        else
        {
            TouchSwipe();
        }
    }


    /*-----------------------------------------------------------------------------------*/
    // ↓スマホでの視点操作+Playerの移動

    /// <summary> スマホをSwipeしたときの水平感度 </summary>
    [Header("水平感度(Phone)") ,SerializeField] float m_xSensitivity = 0.1f;
    /// <summary> 最初にタッチされた座標 </summary>
    Vector3 m_startTouchPos;
    /// <summary> ButtonのGameObject </summary>
    GameObject m_jumpButton = null;

    /// <summary> Joystick </summary>
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
            m_xSensitivity = PlayerSetting.XSensitivity;
            float x = touch.deltaPosition.x; // 偏差分を求める
            // 左右に視点変更する時の角度
            float angleY = this.transform.eulerAngles.y + x * m_xSensitivity;
            // 移動する角度をセットする
            this.transform.eulerAngles = new Vector3(0, angleY, 0);

            m_playerCameraY.PlayerLookVertical(touch);
        }
        if (touch.position.x < Screen.width / 2)
        {
            // Joystickから入力された値
            float h = m_fixedJoystick.Horizontal;
            float v = m_fixedJoystick.Vertical;
            // 入力された値を組み立てる
            Vector3 dir = transform.forward * v + transform.right * h;
            if (dir == Vector3.zero)
            {
                // 方向の入力がニュートラルの時は、y 軸方向の速度を保持するだけ
                m_rb.velocity = new Vector3(0f, m_rb.velocity.y, 0f);
            }
            else
            {
                Vector3 velo = dir * m_movingSpeed; // 入力した方向に移動する
                velo.y = m_rb.velocity.y;   // ジャンプした時の y 軸方向の速度を保持する
                m_rb.velocity = velo;   // 計算した速度ベクトルをセットする
            }
        }
    }


    /*--------------------------------------------------------------------------------------*/
    // ↓Editor上での操作
    /// <summary> 移動速度 </summary>
    [Header("Editor上での移動速度") ,SerializeField] float m_movingSpeed = 5f;
    /// <summary> Jumpするときの力 </summary>
    [Header("Editor上でのジャンプ力") ,SerializeField] float m_jumpPower = 5f;
    /// <summary> 視点移動の感度 </summary>
    [Header("マウス感度（視点移動）"), SerializeField] float m_sensitivity = 3f;
    /// <summary> Jumpされたかどうか判定 </summary>
    bool isJump = true;


    /// <summary>
    /// Editor上でのPlayerの移動
    /// </summary>
    void PlayerMoveOnEditor()
    {
        // 方向の入力を取得し、方向を求める
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        // 入力方向のベクトルを組み立てる
        Vector3 dir = (transform.forward * v + transform.right * h) * m_movingSpeed;

        if (dir == Vector3.zero)
        {
            // 方向の入力がニュートラルの時は、y 軸方向の速度を保持するだけ
            m_rb.velocity = new Vector3(0f, m_rb.velocity.y, 0f);
        }
        else
        {
            Vector3 velo = dir.normalized * m_movingSpeed; // 入力した方向に移動する
            velo.y = m_rb.velocity.y;   // ジャンプした時の y 軸方向の速度を保持する
            m_rb.velocity = velo;   // 計算した速度ベクトルをセットする
        }

        // ジャンプの入力を取得しジャンプする
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        PlayerLookHorizontal();
    }

    /// <summary>
    /// Playerの横の視点を移動する
    /// </summary>
    void PlayerLookHorizontal()
    {
        float xMouse = Input.GetAxis("Mouse X");
        Vector3 newRotation = transform.localEulerAngles;
        newRotation.y += xMouse * m_sensitivity;
        transform.localEulerAngles = newRotation;
    }

    /// <summary>
    /// ジャンプする時に呼び出す
    /// </summary>
    public void Jump()
    {
        // 接地している場合はジャンプする
        if (isJump)
        {
            m_rb.AddForce(Vector3.up * m_jumpPower, ForceMode.Impulse);
        }
        isJump = false;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Object")
        {
            isJump = true;
        }
        if (col.gameObject.tag == "FixedObject")
        {
            isJump = true;
        }
        if (col.gameObject.tag == "FallPos")
        {
            this.transform.position = m_startPos;
        }

        if (col.gameObject.tag == "Goal")
        {
            Debug.Log("Goal");
            GameManager.Instance.SetNowState(GameState.GameClear);
        }
    }
}
