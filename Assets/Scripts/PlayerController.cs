using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    /// <summary> 戦車のスピード </summary>
    [SerializeField] float m_speed = 2;
    /// <summary> タッチされた初期位置 </summary>
    Vector3 m_mousePos;
    Rigidbody m_rb;

    Vector3 m_direction;

    [SerializeField] FixedJoystick m_fixedJoystick = null;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        m_direction = Vector3.forward * m_fixedJoystick.Vertical;
        m_rb.velocity = transform.forward * m_direction.z * m_speed;
    }
}
