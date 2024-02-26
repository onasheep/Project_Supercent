using UnityEngine;

public class Waypoint : MonoBehaviour
{

    private static Waypoint _instance = default;

    public static Waypoint Instance
    {
        get
        {            
            return _instance;
        }
    }


    public GameObject EnterPoint{get { return enterPoint; }}
    public GameObject[] StoragePoint { get { return storagePoint; } }
    public GameObject[] RegisterPoint { get { return registerPoint; } }
    public GameObject LeavePoint { get { return leavePoint; } }
    public GameObject SittingPoint { get { return sittingPoint; } }

    public GameObject tableStackPoint = default;

    private GameObject enterPoint = default;
    private GameObject[] storagePoint = default;
    private GameObject[] registerPoint = default;
    private GameObject leavePoint = default;
    private GameObject sittingPoint = default;


    public bool IsSittingAreaUnlock { get { return isSittingAreaUnlock; } }
    private bool isSittingAreaUnlock = default;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        Init();
    }
    void Start()
    {

    }
    void Init()
    {
        enterPoint = gameObject.FindChildObj("EnterPoint");
        leavePoint = gameObject.FindChildObj("LeavePoint");
        storagePoint = gameObject.FindChildObj("StoragePoints").GetChildrenObjs().ToArray();
        registerPoint = gameObject.FindChildObj("RegisterPoints").GetChildrenObjs().ToArray();
        sittingPoint = gameObject.FindChildObj("SittingPoint");
        tableStackPoint = sittingPoint.FindChildObj("StackPoint");
    }

    private void IsUnlockCheck()
    {
        if (GFunc.GetRootObj("SittingArea").activeInHierarchy == false) { return; }

        isSittingAreaUnlock = true;

    }
    void CheckEmpty(GameObject[] objects)
    {
        foreach (GameObject go in objects)
        {

        }
    }
    // Update is called once per frame
    void Update()
    {
        IsUnlockCheck();
    }

}
