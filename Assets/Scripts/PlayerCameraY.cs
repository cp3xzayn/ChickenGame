using UnityEngine;

public class PlayerCameraY : MonoBehaviour
{
    void Update()
    {
        PlayerLookVertical();
    }

    /// <summary> 視点移動の感度 </summary>
    [Header("マウス感度（視点移動）"), SerializeField] float m_sensitivity = 3f;

    /// <summary>
    /// Playerの縦の視点を移動する
    /// </summary>
    void PlayerLookVertical()
    {
        float yMouse = Input.GetAxis("Mouse Y");
        Vector3 newRotation = transform.localEulerAngles;
        newRotation.x -= yMouse * m_sensitivity;
        transform.localEulerAngles = newRotation;
    }

}
