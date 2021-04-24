using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    /// <summary> XYのパネル </summary>
    [SerializeField] GameObject m_xYPanel = null;
    /// <summary> YZのパネル </summary>
    [SerializeField] GameObject m_yZPanel = null;
    /// <summary> オブジェクトのの位置を決定する時に使用しているカメラ </summary>
    [SerializeField] Camera m_camera = null;

    /// <summary> 掴んでるオブジェクトのポジション </summary>
    Vector3 m_objectPos;

    /// <summary> ObjSetManagerのアタッチされたオブジェクト </summary>
    [SerializeField] GameObject m_objMoveManagerObj = null;
    /// <summary> ObjSetManager </summary>
    ObjMoveManager m_objMoveManager;

    /// <summary> ObjSelectManagerのアタッチされたオブジェクト </summary>
    [SerializeField] GameObject m_objSelectManagerObj = null;
    /// <summary> ObjSelectManager </summary>
    ObjSelectManager m_objSelectManager;


    /// <summary> XYPanelのZ方向のサイズ </summary>
    float m_xYPanelSizeZ;
    /// <summary> YZPanelのX方向のサイズ </summary>
    float m_yZPanelSizeX;

    void Start()
    {
        m_objMoveManager = m_objMoveManagerObj.GetComponent<ObjMoveManager>();
        m_objSelectManager = m_objSelectManagerObj.GetComponent<ObjSelectManager>();
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
                LookPanel(m_xYPanel, false);
                LookPanel(m_yZPanel, false);
                break;
            case GameState.End:
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
        if (m_objMoveManager.nowSetPhase == SetPhase.XYSet)
        {
            if (isWhicXYorYZ)
            {
                distance = DistanceFromObjToPanel(m_objSelectManager.SelectedObjSize.z, m_xYPanelSizeZ);
                Debug.Log("a" + distance);
                LookPanel(m_xYPanel, true);
                LookPanel(m_yZPanel, false);
                // パネルの位置を調整する
                m_xYPanel.transform.position = new Vector3(0, 0, m_objectPos.z + distance);
                isWhicXYorYZ = false;
            }
        }
        else if (m_objMoveManager.nowSetPhase == SetPhase.YZSet)
        {
            if (!isWhicXYorYZ)
            {
                distance = DistanceFromObjToPanel(m_objSelectManager.SelectedObjSize.x, m_yZPanelSizeX);
                LookPanel(m_xYPanel, false);
                LookPanel(m_yZPanel, true);
                // パネルの位置を調整する
                m_yZPanel.transform.position = new Vector3(m_objectPos.x + distance, 0, 0);

                m_camera.transform.position = new Vector3(m_objectPos.x - 10, 0, 0);
                m_camera.transform.rotation = Quaternion.Euler(0, 90, 0);
                isWhicXYorYZ = true;
            }
        }
    }

    /// <summary>
    /// オブジェクトのアクティブを変更する
    /// </summary>
    /// <param name="panel"></param>
    /// <param name="isActive"></param>
    public void LookPanel(GameObject panel,bool isActive)
    {
        panel.SetActive(isActive);
    }

    /// <summary>
    /// Panelの奥行きのサイズを取得する
    /// </summary>
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
    /// <param name="selectObjSize"></param>
    /// <param name="panelSize"></param>
    /// <returns></returns>
    float DistanceFromObjToPanel(float selectObjSize, float panelSize)
    {
        float distance = (selectObjSize + panelSize) / 2;
        return distance;
    }
}
