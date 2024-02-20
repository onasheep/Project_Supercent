using UnityEngine;

public class GameManager : MonoBehaviour
{    
    private void Awake()
    {
        ResourceManager.Init();    
        
    }
    // Start is called before the first frame update
    void Start()
    {

        Debug.Log(ResourceManager.objects[RDefine.OBJECT_CROASSANT].name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
