using UnityEngine;

public class Point : MonoBehaviour
{

    private bool isCustomer = default;
    void Awake()
    {
        isCustomer = false;
    }

    public void EnterCustomer()
    {
        isCustomer = !isCustomer;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("In");
        if (other.CompareTag("customer"))
        {
            Debug.Log("!");
        }
    }


}
