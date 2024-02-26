using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadStorage : MonoBehaviour
{
    [SerializeField]
    private PlayerChecker checker = default;
    private ObjectStacker stacker = default;

    private Croassant[] croassants = default;
    public GameObject[] pos = default;
    private readonly int maxCapacity = 8;
    private bool isGiving = default;
    private Queue<Customer> customers = default;
    private float getDelay = 0.1f;

    WaitForSeconds getDelayTime = default;


    private void Awake()
    {
        Init();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Init()
    {
        croassants = new Croassant[maxCapacity];
        getDelayTime = new WaitForSeconds(getDelay);
        customers = new Queue<Customer>();

        //pos = new Vector3[maxCapacity];
    }

    private void FixedUpdate()
    {

        if (checker.IsEnter)
        {
            if(IsGettable() && isGiving == false)
            {
                stacker = checker.player.objectStacker;
                StartCoroutine(GetBread());
            }
            else if(IsGettable() == false)
            {
                isGiving = false;
                StopCoroutine(GetBread());
            }
         


        }
        else
        {
            isGiving = false;
            StopCoroutine(GetBread());
        }

        if (customers == null || customers.Count < 1) { return; }

        CheckCustomerState();




    }



    private void CheckCustomerState()
    {
        foreach (Customer customer in customers)
        {
            if (customer.state == Customer.STATE.WAIT)
            {
                if (customer.CurCapacity < customer.MaxCapacity)
                {
                    GiveBread(customer);
                }
            }
        }
    }

    public void GiveBread(Customer customer)
    {
        for (int i = 0; i < croassants.Length; i++)
        {
            if (croassants[i] != null)
            {
                customer.CurCapacity++;
                croassants[i].SimulateProjectile(customer.GetStackPos());
                croassants[i].transform.SetParent(customer.stackPos.transform);
                customer.AddCroassant(croassants[i]);
                croassants[i] = null;
                return;
            }
        }
    }

    IEnumerator GetBread()
    {
        while (stacker.CurCapacity > 0)
        {
            isGiving = true;
            Croassant temp = stacker.ReturnCroassant();
            temp.SimulateProjectile(pos[FindEmptyIndex()].transform.position);
            temp.transform.SetParent(null);
            AddStack(temp);
            SoundManager.Instance.OnPlayClip(RDefine.SFX_GET);

            yield return getDelayTime;
        }
        isGiving = false;
    }

    int FindEmptyIndex()
    {
        for(int i = 0; i < croassants.Length;i++)
        {
            if (croassants[i] == null)
            {
                return i;
            }
        }
        return default;
    }
    void AddStack(Croassant croassant_)
    {
        for (int i = 0; i < croassants.Length; i++)
        {
            if (croassants[i] == null)
            {
                croassants[i] = croassant_;
                return;

            }
        }
    }

    bool IsGivable()
    {
        for(int i = 0; i < croassants.Length;i++)
        {
            if (croassants[i] != null)
            {
                return true;
            }
        }
        return false;
    }
    bool IsGettable()
    {
        for (int i = 0; i < croassants.Length; i++)
        {
            if (croassants[i] == null)
            {
                return true;
            }
        }
        return false;
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Customer"))
        {
            customers.Enqueue(other.GetComponent<Customer>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Customer"))
        {
            customers.Dequeue();
        }
    }

}
