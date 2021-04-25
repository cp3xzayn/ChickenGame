using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSelectManager : MonoBehaviour
{
    /// <summary> オブジェクトを選択するためのPanel </summary>
    [SerializeField] GameObject m_selectPanel = null;

    /// <summary> 選択されたオブジェクトの名前 </summary>
    private string m_selectedObjectName;
    /// <summary> 選択されたオブジェクトの名前 </summary>
    public string SelectedObjectName => m_selectedObjectName;

    /// <summary> 選択されたオブジェクトのサイズ </summary>
    private Vector3 m_selectedObjSize;
    /// <summary> 選択されたオブジェクトのサイズ </summary>
    public Vector3 SelectedObjSize => m_selectedObjSize;

    void Update()
    {
        SelectObject();
    }

    /// <summary>
    /// 設置するオブジェクトを決定する
    /// </summary>
    void SelectObject()
    {
        if (GameManager.Instance.NowGameState == GameState.SelectObject)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.collider.gameObject.tag == "Object")
                    {
                        Debug.Log("設置するオブジェクトが選択されました。");
                        m_selectedObjectName = hit.collider.gameObject.name;
                        m_selectPanel.SetActive(false);

                        // 選択されたオブジェクトのサイズを取得する
                        MeshRenderer mesh = hit.collider.gameObject.GetComponent<MeshRenderer>();
                        Bounds bounds = mesh.bounds;
                        m_selectedObjSize = bounds.size;

                        GameManager.Instance.SetNowState(GameState.Prepare);
                    }
                }
            }
        }
    }
}
