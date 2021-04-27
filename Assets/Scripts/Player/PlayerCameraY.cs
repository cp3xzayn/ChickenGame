using UnityEngine;

/// <summary>
/// 垂直方向のカメラの視点移動を管理するクラス
/// </summary>
public class PlayerCameraY : MonoBehaviour
{
    void Update()
    {
        // Editor上で実行したとき
        if (Application.isEditor)
        {
            PlayerLookVerticalOnEditor();
        }
    }

    /// <summary> スマホをSwipeしたときの視点移動感度 </summary>
    [Header("スマホでの視点回転スピード"), SerializeField] float m_ySensitivity = 0.1f;

    /// <summary>
    /// Playerの垂直方向の視点移動（スマホ）
    /// </summary>
    public void PlayerLookVertical(Touch touch)
    {
        m_ySensitivity = PlayerSetting.YSensitivity;
        float y = touch.deltaPosition.y; // 偏差分を求める
        // 左右に視点変更する時の角度
        float angleX = this.transform.eulerAngles.x - y * m_ySensitivity;
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
