using UnityEngine;
using UnityEngine.UI;

public class ObjMoveManager : MonoBehaviour
{
    /// <summary> 生成するオブジェクト </summary>
    GameObject m_object;
    /// <summary> オブジェクトのポジション </summary>
    private Vector3 m_objectPos;
    /// <summary> オブジェクトのポジション </summary>
    public Vector3 CubePos => m_objectPos;
    /// <summary> オブジェクトをつかんでいるか判断する </summary>
    bool isGrabbing = false;
    /// <summary> 掴んでいるオブジェクト </summary>
    GameObject m_grabbingObject;
    [SerializeField] GameObject m_objSelectManagerObj = null;
    ObjSelectManager m_objSelectManager;

    /// <summary> 現在のSetPhaseの状態 </summary>
    private SetPhase m_nowSetPhase;
    /// <summary> 現在のSetPhaseの状態 </summary>
    public SetPhase nowSetPhase => m_nowSetPhase;

    void Awake()
    {
         m_nowSetPhase = SetPhase.Initialize;
    }

    void Start()
    {
        m_objSelectManager = m_objSelectManagerObj.GetComponent<ObjSelectManager>();
    }

    void Update()
    {
        if (GameManager.Instance.NowGameState == GameState.Prepare)
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
    }

    /// <summary>
    /// 掴むオブジェクトを生成する
    /// </summary>
    void InstanceObj()
    {
        string name = m_objSelectManager.SelectedObjectName;
        m_object = Resources.Load<GameObject>(name);
        m_objectPos = Vector3.zero;
        Instantiate(m_object, m_objectPos, Quaternion.identity);
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
                if (hit.collider.gameObject.tag == "Panel")　// PanelにRayが当たったとき
                {
                    if (m_nowSetPhase == SetPhase.XYSet)
                    {
                        // Rayが当たったポジションとObjectの幅の半分から設置位置を決定している
                        // TODO:オブジェクトの幅を取得し、-0.5fのところを変数にしたい。
                        m_grabbingObject.transform.position 
                            = new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.5f); 
                    }
                    else if (m_nowSetPhase == SetPhase.YZSet)
                    {
                        // Rayが当たったポジションとObjectの幅の半分から設置位置を決定している
                        // TODO:オブジェクトの幅を取得し、-0.5fのところを変数にしたい。
                        m_grabbingObject.transform.position
                            = new Vector3(hit.point.x - 0.5f, hit.point.y, hit.point.z); 
                    }
                    
                }
                if (Input.GetMouseButtonUp(0)) // 右クリックを離したら
                {
                    Debug.Log($"Objctを離しました。");
                    m_objectPos = m_grabbingObject.transform.position;
                    isGrabbing = false;
                }
            }
        }
    }

    // 以下ボタンの処理
    public void OnClickYZSetPhase()
    {
        m_nowSetPhase = SetPhase.YZSet;
    }
    public void OnClickEndPhase()
    {
        m_nowSetPhase = SetPhase.SetEnd;
    }
}

/// <summary>
/// Objectを設置する時の状態
/// </summary>
public enum SetPhase
{
    /// <summary> 初期化時 </summary>
    Initialize,
    /// <summary> XY平面での座標決定 </summary>
    XYSet,
    /// <summary> YZ平面での座標決定 </summary>
    YZSet,
    /// <summary> 設置完了時 </summary>
    SetEnd
}
