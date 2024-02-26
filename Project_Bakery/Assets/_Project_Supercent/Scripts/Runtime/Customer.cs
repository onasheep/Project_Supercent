using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using JetBrains.Annotations;

public class Customer : MonoBehaviour
{
    private Animator animator = default;
    private NavMeshAgent navAgent = default;    
    
    public GameObject stackPos = default;
    private Croassant[] croassants = default;
    
    private TMP_Text breadNumText = default;
    private GameObject customerBallon = default;
    private GameObject croassantBallon = default;
    private SpriteRenderer stateImage = default;
    private SpriteRenderer emojiImage = default;

    private float moveSpeed = default;
    public int MaxCapacity { get; private set; }
    public int CurCapacity { get; set; }
    

    private bool isStack = default;
    private bool isWait = default;
    private bool isFullStack = default;
    public bool isRegister =  default;


    public enum STATE
    {
        NONE = -1, MOVE_ENTRANCE, MOVE_STORAGE, MOVE_REGISTER_OUT, MOVE_REGISTER_IN, MOVE_SIT, WAIT, LEAVE,SIT
    }

    public STATE state { get; private set; }
    public STATE prevState { get; private set; }
    void Awake()
    {
        Init();
    }

    void Start()
    {
        ChangeState(STATE.MOVE_ENTRANCE);

    }
    void Init()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        customerBallon = gameObject.FindChildObj("CustomerBallon");
        croassantBallon = customerBallon.FindChildObj("CroassantBallon");
        breadNumText = croassantBallon.FindChildComponent<TMP_Text>("NeedNum");
        stateImage = customerBallon.FindChildComponent<SpriteRenderer>("StateImage");
        emojiImage = gameObject.FindChildComponent<SpriteRenderer>("HappyEmoji");
        moveSpeed = navAgent.speed;
        MaxCapacity = Random.Range(1, 4);
        croassants = new Croassant[MaxCapacity];
        isStack = false;
        state = STATE.NONE;
        customerBallon.SetActive(false);
    }

    public void MoveCroaasant(Vector3 position_)
    {
        croassants = stackPos.GetComponentsInChildren<Croassant>(); 
        for (int i = 0; i < croassants.Length; i++)
        {
            croassants[i].SimulateProjectile(position_, () => CurCapacity--);
           
        }
    }

    public void ClearList()
    {
        foreach(Croassant c in croassants)
        {
            Destroy(c.gameObject);
        }
    }

    public bool isMoveable()
    {
        for (int i = 0; i < croassants.Length; i++)
        {
            if (croassants[i] != null)
            {
                return true;
            }
        }
        return false;

    }
    public void AddCroassant(Croassant croassant)
    {
        for(int i = 0; i < croassants.Length; i++)
        {
            if(croassants[i] == null)
            {
                croassants[i] = croassant;
            }
        }
        
    }

    void ChangeState(STATE state_)
    {
        if(state == state_) { return; }
        prevState = state;
        state = state_;
        switch(state)
        {
            case STATE.NONE:
                break;
            case STATE.MOVE_ENTRANCE:
                StartCoroutine(Moving(Waypoint.Instance.EnterPoint.transform.position));
                break;
            case STATE.MOVE_STORAGE:
                moveSpeed = navAgent.speed;
                breadNumText.text = MaxCapacity.ToString();
                customerBallon.SetActive(true);
                StartCoroutine(Moving(Waypoint.Instance.StoragePoint[0].transform.position));
                break;
            case STATE.MOVE_REGISTER_OUT:
                moveSpeed = navAgent.speed;
                croassantBallon.SetActive(false);
                stateImage.gameObject.SetActive(true);
                StartCoroutine(Moving(Waypoint.Instance.RegisterPoint[0].transform.position));                
                break;
            case STATE.MOVE_REGISTER_IN:
                moveSpeed = navAgent.speed;
                croassantBallon.SetActive(false);
                stateImage.gameObject.SetActive(true);
                StartCoroutine(Moving(Waypoint.Instance.RegisterPoint[1].transform.position));
                break;
            case STATE.MOVE_SIT:
                                        customerBallon.gameObject.SetActive(false);

                moveSpeed = navAgent.speed;
                StartCoroutine(Moving(Waypoint.Instance.SittingPoint.transform.position));
                break;
            case STATE.WAIT:
                moveSpeed = 0;
                if(CurCapacity == 0 && isWait == false && isRegister == false)
                {
                    isWait = true;
                    ChangeState(STATE.MOVE_STORAGE);
                    // Wait 
                }


                break;
            case STATE.LEAVE:
                foreach (Croassant croa in croassants)
                {
                    Destroy(croa.gameObject);
                }
                Instantiate(ResourceManager.objects["Trash"], Waypoint.Instance.tableStackPoint.transform);
                moveSpeed = navAgent.speed;

                StartCoroutine(Moving(Waypoint.Instance.LeavePoint.transform.position));
                break;
            case STATE.SIT:
                StartCoroutine(Sit());
                break;
        }
    }

    void StateProcess()
    {
        switch (state)
        {
            case STATE.WAIT:
                if (isRegister == true)
                {
                    if (prevState == STATE.MOVE_REGISTER_OUT)
                    {
                        customerBallon.gameObject.SetActive(false);
                        emojiImage.gameObject.SetActive(true);
                        StartCoroutine(DelayState(STATE.LEAVE));                     
                    }
                    else if(prevState == STATE.MOVE_REGISTER_IN)
                    {
                        stateImage.sprite = ResourceManager.sprites["TableChair"];
                        if (Waypoint.Instance.IsSittingAreaUnlock)
                        {
                            
                            StartCoroutine(DelayState(STATE.MOVE_SIT));
                        }
                        else
                        {
                            ChangeState(STATE.WAIT);
                        }
                    }
                }
                else if (CurCapacity >= MaxCapacity && isWait == true)
                {
                    isStack = true;
                    isWait = false;
                    float random = Random.value;
                    if(random < 0.1)
                    {
                        StartCoroutine(DelayState(STATE.MOVE_REGISTER_OUT));
                    }
                    else
                    {
                        StartCoroutine(DelayState(STATE.MOVE_REGISTER_IN));

                    }
                }
                break;
            case STATE.LEAVE:

                if (navAgent.remainingDistance == 0)
                {
                    Destroy(this.gameObject);
                }
                break;
            case STATE.MOVE_SIT:
                if((navAgent.remainingDistance < 0.01f))
                {
                    ChangeState(STATE.SIT);
                }
           
                break;
            case STATE.SIT:

                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SwitchAnimation(isStack);
        animator.SetFloat("moveSpeed", moveSpeed);


        StateProcess();

    }

    private void SwitchAnimation(bool isStack_)
    {
        switch (isStack_)
        {
            case false:
                animator.SetBool("isStack", false);
                break;
            case true:
                animator.SetBool("isStack", true);
                break;
        }
    }


    public Vector3 GetStackPos()
    {

        Vector3 temp = default;
        temp = stackPos.transform.position + new Vector3(0f, 0.4f, 0f) * (CurCapacity - 1);
        return temp;


    }

    IEnumerator Moving(Vector3 pos)
    {
        Vector3 dir = pos - this.transform.position;
        float dist = dir.magnitude;
        dir.Normalize();
        while (dist > Mathf.Epsilon)
        {
            float moveDist = moveSpeed * Time.deltaTime;
            if (dist < moveDist)
            {
                moveDist = dist;
            }
            else
            {
                navAgent.SetDestination(pos);
            }
            dist -= moveDist;
            yield return null;
        }
        moveSpeed = 0;
        StartCoroutine(DelayState(STATE.WAIT));
    }
    IEnumerator Sit()
    {
        animator.SetBool("isSitting", true);
        navAgent.baseOffset = 0.75f;
        this.transform.forward = (Waypoint.Instance.SittingPoint.transform.forward);
        for (int i = 0; i < croassants.Length; i++)
        {
            croassants[i].transform.SetParent(Waypoint.Instance.tableStackPoint.transform);
            croassants[i].transform.localPosition = Vector3.zero;
        }
        moveSpeed = 0;
        yield return new WaitForSeconds(4f);
        animator.SetBool("isSitting", false);

        navAgent.baseOffset = 0f;
        isStack = false;

        ChangeState(STATE.LEAVE);

    }

    IEnumerator DelayState(STATE state)
    {
        yield return new WaitForSeconds(2f);
        ChangeState(state);
    }
    void OnDestroy()
    {
        StopAllCoroutines();
    }
}
