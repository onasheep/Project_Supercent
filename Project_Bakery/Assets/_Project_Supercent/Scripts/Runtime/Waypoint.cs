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

    private GameObject enterPoint = default;
    private GameObject[] storagePoint = default;
    private GameObject[] registerPoint = default;
    private GameObject leavePoint = default;

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

    }

    // Update is called once per frame
    void Update()
    {

    }

}
