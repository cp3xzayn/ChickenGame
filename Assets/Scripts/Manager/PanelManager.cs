using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトが移動できる範囲を示すPanelの挙動を管理するクラス
/// </summary>
public class PanelManager : MonoBehaviour
{
    /// <summary> XYのパネル </summary>
    [SerializeField] GameObject m_xYPanel = null;
    /// <summary> YZのパネル </summary>
    [SerializeField] GameObject m_yZPanel = null;
    /// <summary> XYPanelのZ方向のサイズ </summary>
    float m_xYPanelSizeZ;
    /// <summary> YZPanelのX方向のサイズ </summary>
    float m_yZPanelSizeX;
    /// <summary> オブジェクトのの位置を決定する時に使用しているカメラ </summary>
    [SerializeField] Camera m_camera = null;

    /// <summary> 掴んでるオブジェクトのポジション </summary>
    Vector3 m_objectPos;

    /// <summary> ObjSetManager </summary>
    ObjMoveManager m_objMoveManager;
    /// <summary> ObjSelectManager </summary>
    ObjSelectManager m_objSelectManager;

    void Start()
    {
        m_objMoveManager = GetComponent<ObjMoveManager>();
        m_objSelectManager = GetComponent<ObjSelectManager>();
        // それぞれのPanelのサイズを初期化する
        m_xYPanelSizeZ = GetPanelSize(m_xYPanel).z;
        m_yZPanelSizeX = GetPanelSize(m_yZPanel).x;
    }

    void Update()
    {
        switch (GameManager.Instance.NowGameState)
        {
            case GameState.Prepare:
                IndicatePanelsForObjectMove();
                break;
            case GameState.Playing:
                PanelActive(m_xYPanel, false);
                PanelActive(m_yZPanel, false);
                break;
            case GameState.GameClear:
                break;
            default:
                break;
        }
    }

    /// <summary> XYかYZのどちらを操作するか決定する（XY:true,YZ:false） </summary>
    bool isWhicXYorYZ = true;

    /// <summary>
    /// オブジェクトを動かすことができる範囲を表示するPanelの処理
    /// </summary>
    void IndicatePanelsForObjectMove()
    {
        // 選択されたオブジェクトとPanel距離
        float distance;
        m_objectPos = m_objMoveManager.CubePos;
        if (m_objMoveManager.NowSetPhase == SetPhase.XYSet)
        {
            if (isWhicXYorYZ)
            {
                distance = DistanceFromObjToPanel(m_objSelectManager.SelectedObjSize.z, m_xYPanelSizeZ);
                PanelActive(m_xYPanel, true);
                PanelActive(m_yZPanel, false);
                // パネルの位置を調整する
                m_xYPanel.transform.position = new Vector3(0, 0, m_objectPos.z + distance);

                //　カメラの位置、向いてる方向を変える
                //　カメラを選択されたオブジェクトから距離10離すために-10している
                m_camera.transform.position = new Vector3(0, 0, m_objectPos.z -10); 
                m_camera.transform.rotation = Quaternion.Euler(0, 0, 0);

                isWhicXYorYZ = false;
            }
        }
        else if (m_objMoveManager.NowSetPhase == SetPhase.YZSet)
        {
            if (!isWhicXYorYZ)
            {
                distance = DistanceFromObjToPanel(m_objSelectManager.SelectedObjSize.x, m_yZPanelSizeX);
                PanelActive(m_xYPanel, false);
                PanelActive(m_yZPanel, true);
                // パネルの位置を調整する
                m_yZPanel.transform.position = new Vector3(m_objectPos.x + distance, 0, 0);

                //　カメラの位置、向いてる方向を変える
                //　カメラを選択されたオブジェクトから距離10離すために-10している
                m_camera.transform.position = new Vector3(m_objectPos.x - 10, 0, 0);
                m_camera.transform.rotation = Quaternion.Euler(0, 90, 0);

                isWhicXYorYZ = true;
            }
        }
    }

    /// <summary>
    /// Panelのアクティブを変更する
    /// </summary>
    /// <param name="panel"></param>
    /// <param name="isActive"></param>
    public void PanelActive (GameObject panel,bool isActive)
    {
        panel.SetActive(isActive);
    }

    /// <summary>
    /// Panelの奥行きのサイズを取得する
    /// </summary>
    /// <param name="panel">サイズを取得するGameObject</param>
    /// <returns>取得したオブジェクトのサイズ</returns>
    Vector3 GetPanelSize(GameObject panel)
    {
        MeshRenderer mesh = panel.GetComponent<MeshRenderer>();
        Bounds bounds = mesh.bounds;
        Vector3 objSize = bounds.size;

        return objSize;
    }

    /// <summary>
    /// ObjとPnaelの距離を計算し返す
    /// </summary>
    /// <param name="selectObjSize">選択されたオブジェクトのサイズ</param>
    /// <param name="panelSize">パネルの厚さ</param>
    /// <returns></returns>
    float DistanceFromObjToPanel(float selectObjSize, float panelSize)
    {
        float distance = (selectObjSize + panelSize) / 2;
        return distance;
    }
}
