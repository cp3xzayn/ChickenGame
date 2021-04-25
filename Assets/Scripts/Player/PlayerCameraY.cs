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
    [Header("スマホでの視点回転スピード"), SerializeField] float m_swipeTurnSpeed = 0.1f;

    /// <summary>
    /// Playerの縦の視点移動（スマホ）
    /// </summary>
    public void PlayerLookVertical(Touch touch)
    {
        float y = touch.deltaPosition.y; // 偏差分を求める
        // 左右に視点変更する時の角度
        float angleX = this.transform.eulerAngles.x - y * m_swipeTurnSpeed;
        // 移動する角度をセットする
        this.transform.localEulerAngles = new Vector3(angleX, 0, 0);
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
