using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pos : MonoBehaviour
{
    private PlayerChecker checker;
    [SerializeField]
    private GameObject[] paperbagPos = default;
    [SerializeField]
    private BoxCollider[] customerCheckCol = default;

    private List<Customer> customers = default;
    private MoneyPile moneyPile = default;
    private GameObject moneyPos = default;

    private bool isCustomerEnter = default;
    private bool isRegistering = default;
    void Awake()
    {
        Init();
    }

    void Init()
    {
        checker = gameObject.FindChildComponent<PlayerChecker>("PlayerChecker");
        customerCheckCol = gameObject.GetComponents<BoxCollider>();
        isCustomerEnter = false;
        isRegistering = false;
        customers = new List<Customer>();
        moneyPos = gameObject.FindChildObj("MoneyPos");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(customers.Count == 0) { return; }
        if (checker.IsEnter)
        {
            if(GetFirstCustomer().prevState == Customer.STATE.MOVE_REGISTER_OUT)
            {
                if (isRegistering == false)
                {
                    StartCoroutine(SellOutProcess());
                }
            }
            else
            {
                if(isRegistering == false)
                {
                    GetFirstCustomer().isRegister = true;
                }
            }
            
        }
    }


    IEnumerator SellOutProcess()
    {
        Customer customer = GetFirstCustomer();
        GameObject paperBag = Instantiate(ResourceManager.objects["PaperBag"], paperbagPos[1].transform.position, Quaternion.Euler(0f, 90f, 0f));        
        while (customer.CurCapacity > 0)
        {
            isRegistering = true;
            if (!customers.Contains(customer)) 
            {
                isRegistering = false;
                yield break;             
            }
            customer.MoveCroaasant(paperbagPos[1].transform.position);
            yield return new WaitForSeconds(1f);
        }
        paperBag.transform.SetParent(customer.stackPos.transform);
        customers.Remove(customer);
        customer.isRegister = true;
        customer.ClearList();
        isRegistering = false;
    }
    
    void CreatMoneyPile()
    {
        moneyPile = Instantiate(ResourceManager.objects["MoneyPile"],moneyPos.transform).GetComponent<MoneyPile>();
    }


    Customer GetFirstCustomer()
    {
        for(int i = 0; i < customers.Count; i++) 
        {
            if (customers[i] != null)
            {
                return customers[i];
            }

        }
        return null;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Customer"))
        {
            customers.Add(other.gameObject.GetComponent<Customer>());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Customer"))
        {
            if(moneyPile == null)
            {
                CreatMoneyPile();
            }
            Customer customer = other.gameObject.GetComponent<Customer>();
            moneyPile.SpawnMoney(customer.MaxCapacity * 5);
            SoundManager.Instance.OnPlayClip(RDefine.SFX_CASH);
            if(customer.stackPos.FindChildObj("PaperBag(Clone)") == null) { return; }
            customer.stackPos.FindChildObj("PaperBag(Clone)").transform.localPosition = Vector3.zero;
        }
    }
}
