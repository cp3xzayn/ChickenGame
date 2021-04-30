using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCylinder : MonoBehaviour
{
    [SerializeField] float m_turnSpeed = 120f;

    void Update()
    {
        if (GameManager.Instance.NowGameState == GameState.Playing)
        {
            this.gameObject.transform.Rotate(new Vector3(1, 0, 0) * m_turnSpeed * Time.deltaTime);
        }
    }
}
