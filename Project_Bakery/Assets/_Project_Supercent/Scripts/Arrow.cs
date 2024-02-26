using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public enum STATE
    {
        NONE = -1, OVEN, STORAGE, POS, MONEYPILE, TEMPAREA
    }
    STATE arrowState = STATE.NONE;

    private Vector3 targetPos = default;
    public Vector3 arrowPos = default;
    private Transform breadOven = default;
    private Transform breadStorage = default;
    private Transform pos = default;
    private Transform tempArea = default;
    private Transform moneyPile = default;
    private bool movingToA = true;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void ChangeState(STATE state_)
    {
        if (arrowState == state_) { return; }
        arrowState = state_;
        switch (arrowState)
        {
            case STATE.NONE:
                break;
            case STATE.OVEN:
                break;
            case STATE.STORAGE:
                break;
            case STATE.POS:
                break;
            case STATE.MONEYPILE:
                break;
            case STATE.TEMPAREA:
                break;

        }
    }
    void Init()
    {
        ChangeState(STATE.OVEN);
        arrowPos = transform.position + Vector3.up * 3f;
        targetPos = transform.position - new Vector3(0f, 1f, 0f);
        MovePosition();
        breadOven = GFunc.GetRootObj("BreadOven").transform;
        breadStorage = GFunc.GetRootObj("BreadStorage").transform;
        pos = GFunc.GetRootObj("POS").transform;
        tempArea = GFunc.GetRootObj(("TempArea")).transform; ;
        moneyPile = pos.gameObject.FindChildObj("MoneyPos").transform;

    }
    // Update is called once per frame
    void Update()
    {
        StopAllCoroutines();
        this.transform.position = breadStorage.position;
       arrowPos = breadStorage.position;
    }

    
    public void MovePosition()
    {
        StartCoroutine(MoveArrow());
    }

    IEnumerator MoveArrow()
    {
        while (true) 
        {
            
            Vector3 targetPoint = movingToA ? arrowPos : targetPos;

            float distance = Vector3.Distance(transform.position, targetPoint);

            while (distance > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPoint, Time.deltaTime);

                distance = Vector3.Distance(transform.position, targetPoint);

                yield return null;
            }

            movingToA = !movingToA;
        }
    }
}
