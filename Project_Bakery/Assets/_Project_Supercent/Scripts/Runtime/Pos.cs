using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pos : MonoBehaviour
{
    private PlayerChecker checker;
    [SerializeField]
    private GameObject[] paperbagPos = default;
    private Customer customer = default;
    private List<Croassant> croassants = default;

    private bool isCustomerEnter = default;
    private bool isRegistering = default;
    void Awake()
    {
        Init();
    }

    void Init()
    {
        checker = gameObject.FindChildComponent<PlayerChecker>("PlayerChecker");
        isCustomerEnter = false;
        isRegistering = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (checker.IsEnter && isCustomerEnter)
        {
            if (customer != null && isRegistering == false)
            {
                isRegistering = true;
                StartCoroutine(SellProcess());
            }
        }
    }


    
    void SpawnPaperBag()
    {

    }

    IEnumerator SellProcess()
    {
        GameObject paperBag = Instantiate(ResourceManager.objects["PaperBag"], paperbagPos[1].transform.position,Quaternion.identity);
        while (customer.isMoveable())
        {
            customer.MoveCroaasant().SimulateProjectile(paperbagPos[1].transform.position);
            yield return new WaitForSeconds(0.1f);
        }
        paperBag.transform.SetParent(customer.stackPos.transform);
        paperBag.transform.localPosition = Vector3.zero;
        customer.isRegister = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Customer"))
        {
            customer = other.gameObject.GetComponent<Customer>();
            isCustomerEnter = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Customer"))
        {
            isCustomerEnter = false;
        }
    }
}
