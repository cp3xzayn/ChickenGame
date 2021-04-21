using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary> 戦車のスピード </summary>
    [SerializeField] float m_speed = 2;
    /// <summary> JoyStick </summary>
    [SerializeField] FloatingJoystick m_floatingJoystick = null;
    Rigidbody m_rb;
    /// <summary> 入力された方向ベクトル </summary>
    private Vector3 m_direction;
    /// <summary> 入力された方向ベクトル </summary>
    public Vector3 Direction => m_direction;

    void Start()
    {
        m_rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (m_direction != Vector3.zero)
        {
            transform.localRotation = Quaternion.LookRotation(m_direction);
        }
    }

    void FixedUpdate()
    {
        m_direction = Vector3.forward * m_floatingJoystick.Vertical + Vector3.right * m_floatingJoystick.Horizontal;
        m_rb.velocity = m_direction * m_speed;
    }
}
