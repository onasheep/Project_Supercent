using System.Collections;
using UnityEngine;

public class BreadStorage : MonoBehaviour
{
    [SerializeField]
    private PlayerChecker checker = default;
    private ObjectStacker stacker = default;

    private Croassant[] croassants = default;
    public Vector3[] pos = default;
    private readonly int maxCapacity = 8;
    private bool isGiving = default;

    private float giveDelay = 0.5f;

    WaitForSeconds makeDelayTime = default;
    WaitForSeconds giveDelayTime = default;


    private void Awake()
    {
        Init();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void Init()
    {
        croassants = new Croassant[maxCapacity];
        giveDelayTime = new WaitForSeconds(giveDelay);

        //pos = new Vector3[maxCapacity];
    }

    private void FixedUpdate()
    {
        if (checker.IsEnter)
        {
            stacker = checker.player.objectStacker;
            if (croassants.Length < 1 )
            {

                StartCoroutine(GiveBread());

            }

        }
        else
        {
            isGiving = false;
            StopCoroutine(GiveBread());
        }
    }

    //IEnumerator GetBread()
    //{
    //    while(stacker.CurCapacity != 0)
    //    {
    //        stacker.
    //        yield return giveDelayTime;
    //    }
    //}

    IEnumerator GiveBread()
    {
        isGiving = true;
        while (stacker.CurCapacity < stacker.MaxCapacity)
        {
            int tempIdx = GetCroassantIdx();
            if (tempIdx == -1) { yield break; }

            croassants[tempIdx].transform.SetParent(stacker.transform);
            stacker.AddToStack(croassants[tempIdx]);
            croassants[tempIdx].GetComponent<Croassant>().SimulateProjectile(pos[tempIdx]);
            croassants[tempIdx] = null;
            yield return giveDelayTime;
        }
        isGiving = false;
    }

    int GetCroassantIdx()
    {
        for (int i = 0; i < croassants.Length; i++)
        {
            if (croassants[i] != null)
            {
                return i;
            }
        }
        return -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
