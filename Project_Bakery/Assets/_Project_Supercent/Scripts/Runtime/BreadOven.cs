using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadOven : MonoBehaviour
{
    [SerializeField]
    private float breadDelay = 2f;
    private int maximumCapacity = 8;
    private int CroassantNum = default;
    private GameObject[] croassnts = default;

    WaitForSeconds delayTime = default;

    public Transform spawnPoint;

    [SerializeField]
    private PlayerChecker checker = default;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        StartCoroutine(MakeBread());

    }

    void Init()
    {
        delayTime = new WaitForSeconds(breadDelay);
        croassnts = new GameObject[maximumCapacity];
        CroassantNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(checker.IsEnter && croassnts.Length > 0)
        {
            StartCoroutine(GiveBread());
        }
    }


    IEnumerator GiveBread()
    {
        int idx = 0;
        while(idx < 1)
        {
            croassnts[idx].transform.SetParent(checker.player.spawnPoint.transform);

            idx++;
            yield return null;
        }
                    
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
        for(int i = 0; i < croassnts.Length; i++)
        {
            if (croassnts[i] == null)
            {
                croassnts[i] = croassant_;
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
