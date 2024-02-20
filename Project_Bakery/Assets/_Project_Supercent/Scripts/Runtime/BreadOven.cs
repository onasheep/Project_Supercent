using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadOven : MonoBehaviour
{
    [SerializeField]
    private float breadDelay = 2f;
    private int maximumCapacity = 8;
    private int CroassantNum = default;
    private GameObject[] croaasnts = default;

    WaitForSeconds delayTime = default;

    public Transform spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        StartCoroutine(MakeBread());

    }

    void Init()
    {
        delayTime = new WaitForSeconds(breadDelay);
        croaasnts = new GameObject[maximumCapacity];
        CroassantNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MakeBread()
    {
        while(CheckFull())
        {
            GameObject tempObj = Instantiate(ResourceManager.objects[RDefine.OBJECT_CROASSANT], spawnPoint.position ,Quaternion.identity);
            tempObj.GetComponent<Croassant>().Spawn(spawnPoint.transform.forward);
            AddCroassant(tempObj);
            CroassantNum++;
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

    // TODO : 메서드 이름과 반환형이 맞지 않으므로 수정해야 함
    // Full이 되면 false를 반환하는 상황임
    bool CheckFull()
    {
        if(CroassantNum >= maximumCapacity)
        {
            return false;
        }
        return true;
    }
}
