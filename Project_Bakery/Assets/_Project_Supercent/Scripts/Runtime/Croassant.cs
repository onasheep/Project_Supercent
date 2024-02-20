using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Croassant : MonoBehaviour
{
    private Rigidbody rigid = default;
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 15f;

    private void Awake()
    {
        Init();
    }
    void Start()
    {
        
    }
    
    void Init()
    {
        rigid = GetComponent<Rigidbody>();  
    }

    public void Spawn(Vector3 dir)
    {
        rigid.AddForce(dir * speed, ForceMode.VelocityChange);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
