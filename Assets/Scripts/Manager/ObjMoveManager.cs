using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// オブジェクトを選択し、位置を変えるためのクラス
/// </summary>
public class ObjMoveManager : MonoBehaviour
{
    /// <summary> 生成するオブジェクト </summary>
    GameObject m_object;
    /// <summary> オブジェクトのポジション </summary>
    private Vector3 m_objectPos;
    /// <summary> オブジェクトのポジション </summary>
    public Vector3 ObjectPos => m_objectPos;
    /// <summary> オブジェクトをつかんでいるか判断する </summary>
    bool isGrabbing = false;
    /// <summary> 掴んでいるオブジェクト </summary>
    GameObject m_grabbingObject;
    /// <summary> 掴んでいるオブジェクトのサイズ </summary>
    Vector3 m_grabbingObjSize;

    /// <summary> SetPhaseを変更するButton </summary>
    [SerializeField] GameObject m_setPhaseButtons = null;
    /// <summary> Tutorialを表示するPanel </summary>
    [SerializeField] GameObject m_tutorialPanel = null;

    /// <summary> ObjSelectManager </summary>
    ObjSelectManager m_objSelectManager;
    /// <summary> 現在のSetPhaseの状態 </summary>
    private SetPhase m_nowSetPhase;
    /// <summary> 現在のSetPhaseの状態 </summary>
    public SetPhase NowSetPhase => m_nowSetPhase;


    void Awake()
    {
         m_nowSetPhase = SetPhase.Initialize;
    }

    void Start()
    {
        m_objSelectManager = GetComponent<ObjSelectManager>();
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
                    GameManager.Instance.SetNowState(GameState.CountDownPlaying);
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// このメソッドを用いてSetPhaseを変更する
    /// </summary>
    /// <param name="phase"></param>
    public void SetSetPhase(SetPhase phase)
    {
        m_nowSetPhase = phase;
        OnChangeSetPhase(m_nowSetPhase);
    }

    /// <summary>
    /// SetPhaseが変わったときの処理（一度だけ呼ばれる）
    /// </summary>
    /// <param name="phase"></param>
    void OnChangeSetPhase(SetPhase phase)
    {
        switch (phase)
        {
            case SetPhase.Tutorial:
                StartTutorial();
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
        string name = m_objSelectManager.SelectedObjectName;
        m_object = Resources.Load<GameObject>(name);
        m_objectPos = Vector3.zero;
        Instantiate(m_object, m_objectPos, Quaternion.identity);
        m_setPhaseButtons.SetActive(true);

        SetSetPhase(SetPhase.Tutorial);
    }

    /// <summary>
    /// Objectを選択し、ドラッグアンドドロップする
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
                        // ObjectSize(m_grabbingObject).z /2 はオブジェクトの幅から半径を取得している。
                        m_grabbingObject.transform.position 
                            = new Vector3(hit.point.x, hit.point.y, hit.point.z - ObjectSize(m_grabbingObject).z /2); 
                    }
                    else if (m_nowSetPhase == SetPhase.YZSet)
                    {
                        // Rayが当たったポジションとObjectの幅の半分から設置位置を決定している
                        m_grabbingObject.transform.position
                            = new Vector3(hit.point.x - ObjectSize(m_grabbingObject).x / 2, hit.point.y, hit.point.z); 
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

    /// <summary>
    /// 掴んでいるオブジェクトの幅を取得し返す
    /// </summary>
    /// <param name="gameObject">掴んでいるオブジェクト</param>
    /// <returns></returns>
    Vector3 ObjectSize(GameObject gameObject)
    {
        MeshRenderer mesh = gameObject.GetComponent<MeshRenderer>();
        Bounds bounds = mesh.bounds;
        m_grabbingObjSize = bounds.size;
        return m_grabbingObjSize;
    }

    /// <summary>
    /// チュートリアルを開始する
    /// </summary>
    void StartTutorial()
    {
        if (PlayerSetting.IsTutorial)
        {
            m_tutorialPanel.transform.localScale = Vector3.one;
        }
        else
        {
            SetSetPhase(SetPhase.XYSet);
        }
    }


    // ↓以下ボタンの処理
    public void OnClickXYSetPhase()
    {
        if (m_nowSetPhase != SetPhase.Tutorial)
        {
            m_nowSetPhase = SetPhase.XYSet;
        }
    }
    public void OnClickYZSetPhase()
    {
        if (m_nowSetPhase != SetPhase.Tutorial)
        {
            m_nowSetPhase = SetPhase.YZSet;
        }
    }

    /// <summary>
    /// SetPhaseがEndになった時の処理
    /// </summary>
    public void OnClickEndPhase()
    {
        if (m_nowSetPhase != SetPhase.Tutorial)
        {
            m_nowSetPhase = SetPhase.SetEnd;
            m_setPhaseButtons.SetActive(false);
        }
    }

}

/// <summary>
/// Objectを設置する時の状態
/// </summary>
public enum SetPhase
{
    /// <summary> 初期化時 </summary>
    Initialize,
    /// <summary> Tutorial </summary>
    Tutorial,
    /// <summary> XY平面での座標決定 </summary>
    XYSet,
    /// <summary> YZ平面での座標決定 </summary>
    YZSet,
    /// <summary> 設置完了時 </summary>
    SetEnd
}
