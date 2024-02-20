using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadOven : MonoBehaviour
{
    [SerializeField]
    private float breadDelay = 2f;
    private int maximumCroassant = 8;
    private GameObject[] croaasnts = default;

    WaitForSeconds delayTime = default;

    // TODO : Croaasnat 오브젝트가져오기
    public GameObject croassant;
    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        delayTime = new WaitForSeconds(breadDelay);
        croaasnts = new GameObject[maximumCroassant];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MakeBread()
    {
        while(true)
        {
            GameObject tempObj = Instantiate(croassant, spawnPoint.position ,Quaternion.identity);
            AddCroassant(tempObj);
            yield return delayTime;
        }
    }

    void AddCroassant(GameObject croassant_)
    {
        for(int i = 0; i < croaasnts.Length; i++)
        {
            if (croaasnts[i] == null)
            {
                croaasnts[i] = croassant_;
            }
        }
    }
}
