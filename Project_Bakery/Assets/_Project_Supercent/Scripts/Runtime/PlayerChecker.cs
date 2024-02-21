using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    public bool IsEnter { get { return isEnter; } }           
    private bool isEnter = default;

    public PlayerController player = default;
    
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        isEnter = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player = other.GetComponent<PlayerController>();    
            //Debug.Log($"{other.gameObject.name} Enter");
            isEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //Debug.Log($"{other.gameObject.name} Exit");
            isEnter = false;

        }
    }

}
