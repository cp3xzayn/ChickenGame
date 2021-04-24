using UnityEngine;

public class PlayerCameraY : MonoBehaviour
{
    void Update()
    {
        if (Application.isEditor)
        {
            PlayerLookVerticalOnEditor();
        }
    }

    /// <summary> スマホをSwipeしたときの回転スピード </summary>
    // [SerializeField] float m_swipeTurnSpeed = 0.1f;

    /// <summary>
    /// Playerの縦の視点移動（スマホ）
    /// </summary>
    public void PlayerLookVertical(Touch touch)
    {
    }

    /// <summary> 視点移動の感度 </summary>
    [Header("マウス感度（視点移動）"), SerializeField] float m_sensitivity = 3f;

    /// <summary>
    /// Playerの縦の視点を移動する
    /// </summary>
    void PlayerLookVerticalOnEditor()
    {
        float yMouse = Input.GetAxis("Mouse Y");
        Vector3 newRotation = transform.localEulerAngles;
        newRotation.x -= yMouse * m_sensitivity;
        transform.localEulerAngles = newRotation;
    }

}
