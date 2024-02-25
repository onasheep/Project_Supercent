using System.Collections;
using UnityEngine;

public class BreadOven : MonoBehaviour
{
    private int maxCapacity = 8;
    private Croassant[] croassants = default;
    
    
    [SerializeField]
    private float makeDelay = 2f;
    private float giveDelay = 1f;

    WaitForSeconds makeDelayTime = default;
    WaitForSeconds giveDelayTime = default;


    public Transform spawnPoint;

    [SerializeField]
    private PlayerChecker checker = default;
    private ObjectStacker stacker = default;

    private bool isGiving = default;

    private bool isEnter = default;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        StartCoroutine(MakeBread());
    }

    void Init()
    {
        makeDelayTime = new WaitForSeconds(makeDelay);
        giveDelayTime = new WaitForSeconds(giveDelay);
        croassants = new Croassant[maxCapacity];

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        isEnter = checker.IsEnter;

        if (isEnter)
        {
            stacker = checker.player.objectStacker;
            if (isGiving == false)
            {
                StartCoroutine(GiveBread());
            }
            else if(GetCroassantIdx() == -1)
            {
                isGiving = false;
                StartCoroutine(MakeBread());
            }

        }
        else
        {
            Debug.Log("isEnter = false");
            isGiving = false;
            StopCoroutine(GiveBread());
        }

    }

    private
    IEnumerator GiveBread()
    {
        while (stacker.CurCapacity < stacker.MaxCapacity)
        {
            

            isGiving = true;

            int tempIdx = GetCroassantIdx();
            if(tempIdx == -1) { yield break; }

            croassants[tempIdx].transform.SetParent(stacker.transform);
            stacker.AddToStack(croassants[tempIdx]);
            croassants[tempIdx].GetComponent<Croassant>().SimulateProjectile(stacker.GetStackPos());
            croassants[tempIdx] = null;
            yield return giveDelayTime;
        }
        isGiving = false;     
    }


    IEnumerator MakeBread()
    {
        while(IsEmpty())
        {
            GameObject tempObj = Instantiate(ResourceManager.objects[RDefine.OBJECT_CROASSANT], spawnPoint.position ,Quaternion.identity);
            Croassant croassant = tempObj.GetComponent<Croassant>();
            croassant.Spawn(spawnPoint.transform.forward);
            AddCroassant(croassant);
            yield return makeDelayTime;
        }
    }

    int GetCroassantIdx()
    {
        for(int i = 0; i <  croassants.Length; i++)
        {
            if (croassants[i] != null)
            {
                return i;
            }
        }
        return -1;
    }

    void AddCroassant(Croassant croassant_)
    {
        for(int i = 0; i < croassants.Length; i++)
        {
            if (croassants[i] == null)
            {
                croassants[i] = croassant_;
                return;
            }
            
        }
    }
    bool isExist()
    {
        foreach(Croassant croassant in croassants)
        {
            if(croassant != null)
            {
                return true;
            }
        }
        return false;

    }

    bool IsEmpty()
    {
        foreach(Croassant croassant in croassants)
        {
            if(croassant == null)
            {
                return true;
            }
            
        }
        return false;
    }
}
