using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStacker : MonoBehaviour
{

    // 추후 확장성을 고려해서 Stack이 가능한 오브젝트들의 상위 클래스 타입으로 가져오기
    public Croassant[] stack = default;

    public List<GameObject> stackPos;

    public int MaxCapacity { get { return maxCapacity; } }
    public int CurCapacity { get { return curCapacity; } }    
    public bool IsStack { get { return isStack; } }

    private readonly int maxCapacity = 8;
    [SerializeField]
    private int curCapacity = default;
    [SerializeField]
    private bool isStack = default;
    private int stackPointIndex = default;

    bool isFull = default;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        stack = new Croassant[maxCapacity];
        isFull = false;      
    }
    // Update is called once per frame
    void Update()
    {

        CheckArray();               
    }

    private bool CheckArray()
    {
        for(int i = 0; i < stack.Length; i++)
        {
            if (stack[i] != null)
            {
                isStack = true;
                return isFull;
            }
        }
        isStack = false;
        return isStack;
    }
    public Vector3 GetStackPos()
    {
        for(int i = 0; i < stack.Length; i++)
        {
            if(stack[i] != null)
            {
                Vector3 temp = default;
                temp = this.transform.position + new Vector3(0f,0.2f,0f) * stackPointIndex;
                return temp;
            }
        }
        // 임시
        return default;


    }

    public void AddToStack(Croassant croassant_)
    {
        for (int i = 0; i < stack.Length; i++)
        {
            if (stack[i] == null)
            {
                stack[i] = croassant_;
                stackPointIndex = i;
                curCapacity++;
                return;
            }
        }
    }

    public void DeleteToStack(int idx_)
    {
        stack[idx_] = null;
        curCapacity--;
    }

    public Croassant ReturnCroassant()
    {
        for(int i = stack.Length - 1; i >= 0; i--)
        {
            if (stack[i] != null)
            {
                Croassant temp = stack[i];
                DeleteToStack(i);
                return temp;
            }
        }
        return default;
    }
}
