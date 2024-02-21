using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadOven : MonoBehaviour
{
    [SerializeField]
    private float breadDelay = 2f;
    private int maxCapacity = 8;
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
        croassnts = new GameObject[maxCapacity];
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
            croassnts[idx].transform.SetParent(checker.player.objectStacker.transform);
            croassnts[idx].GetComponent<Croassant>().SimulateProjectile(checker.player.objectStacker.transform.position);        
            idx++;
            croassnts[idx] = null;
            yield return null;
        }
                    
    }


    IEnumerator MakeBread()
    {
        while(IsEmpty())
        {
            GameObject tempObj = Instantiate(ResourceManager.objects[RDefine.OBJECT_CROASSANT], spawnPoint.position ,Quaternion.identity);
            tempObj.GetComponent<Croassant>().Spawn(spawnPoint.transform.forward);
            AddCroassant(tempObj);
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
                return;
            }
            
        }
    }

    bool IsEmpty()
    {
        foreach(GameObject croassant in croassnts)
        {
            if(croassant == null)
            {
                return true;
            }
            
        }
        return false;
    }
}
