using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerPC : MonoBehaviour
{
    /// <summary> 移動速度 </summary>
    [SerializeField] float m_movingSpeed = 5f;
    /// <summary> Jumpするときの力 </summary>
    [SerializeField] float m_jumpPower = 5f;
    /// <summary> Jumpされたかどうか判定 </summary>
    bool isJump = true;
    /// <summary> Playerの初期ポジション </summary>
    Vector3 m_startPos;

    Rigidbody m_rb;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_startPos = this.transform.position;
    }

    void Update()
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

        // ジャンプの入力を取得し、接地している場合はジャンプする
        if (Input.GetButtonDown("Jump") && isJump)
        {
            Jump();
        }

        PlayerLookHorizontal();
    }

    /// <summary> 視点移動の感度 </summary>
    [Header("マウス感度（視点移動）"),SerializeField] float m_sensitivity = 3f;

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
    void Jump()
    {
        m_rb.AddForce(Vector3.up * m_jumpPower, ForceMode.Impulse);
        isJump = false;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Object")
        {
            isJump = true;
        }
        if (col.gameObject.tag == "FallPos")
        {
            this.gameObject.transform.position = m_startPos;
        }
    }

}
