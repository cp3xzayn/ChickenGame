using UnityEngine;
using UnityEngine.UI;

public class ObjectSetManager : MonoBehaviour
{
    /// <summary> 生成するオブジェクト </summary>
    GameObject m_cube;
    /// <summary> オブジェクトのポジション </summary>
    private Vector3 m_cubePos;
    public Vector3 CubePos => m_cubePos;
    /// <summary> オブジェクトをつかんでいるか判断する </summary>
    bool isGrabbing = false;
    /// <summary> 掴んでいるオブジェクト </summary>
    GameObject m_grabbingObject;

    private SetPhase m_nowSetPhase;

    public SetPhase nowSetPhase => m_nowSetPhase;

    void Awake()
    {
         m_nowSetPhase = SetPhase.Initialize;
    }

    void Update()
    {
        switch (m_nowSetPhase)
        {
            case SetPhase.Initialize:
                Debug.Log("SetPhase.Initialize");
                InstanceObj();
                break;
            case SetPhase.XYSet:
                Debug.Log("SetPhase.XYSet");
                SwipeObject();
                break;
            case SetPhase.YZSet:
                Debug.Log("SetPhase.YZSet");
                SwipeObject();
                break;
            case SetPhase.SetEnd:
                Debug.Log("SetPhase.SetEnd");
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 掴むオブジェクトを生成する
    /// </summary>
    void InstanceObj()
    {
        m_cube = Resources.Load<GameObject>("Cube");
        m_cubePos = Vector3.zero;
        Instantiate(m_cube, m_cubePos, Quaternion.identity);
        m_nowSetPhase = SetPhase.XYSet;
    }

    /// <summary>
    /// Objectをドラッグアンドドロップする
    /// </summary>
    void SwipeObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.tag == "Object")
                {
                    isGrabbing = true;
                    Debug.Log($"Objectをつかみました。");
                    m_grabbingObject = hit.collider.gameObject;
                }
            }
        }

        if (isGrabbing)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "Panel")　// PanelにRayが当たったとき
                {
                    Debug.Log(m_grabbingObject.name);
                    m_grabbingObject.transform.position = hit.point; // Objを移動する
                }
                if (Input.GetMouseButtonUp(0)) // 右クリックを離したら
                {
                    Debug.Log($"Objctを離しました。");
                    isGrabbing = false;
                }
            }
        }
    }
}

public enum SetPhase
{
    Initialize,
    XYSet,
    YZSet,
    SetEnd
}
