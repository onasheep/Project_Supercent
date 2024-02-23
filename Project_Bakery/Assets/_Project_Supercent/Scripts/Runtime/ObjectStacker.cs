using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStacker : MonoBehaviour
{

    // ���� Ȯ�强�� ����ؼ� Stack�� ������ ������Ʈ���� ���� Ŭ���� Ÿ������ ��������
    private Croassant[] stack = default;

    public List<GameObject> stackPos;

    public int MaxCapacity { get { return maxCapacity; } }
    public int CurCapacity { get { return curCapacity; } }    
    public bool IsStack { get { return isStack; } }

    private readonly int maxCapacity = 8;
    [SerializeField]
    private int curCapacity = default;
    [SerializeField]
    private bool isStack = default;
    int idx = 0;

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
                idx++;
                Debug.Log(stackPos[idx].transform.position);
                Debug.Log(idx);
                temp = stackPos[idx].transform.position + new Vector3(0f,0.2f,0f) * idx;
                return temp;
            }
        }
        // �ӽ�
        return default;    
        
        
    }

    public void AddToStack(Croassant croassant_)
    {
        for(int i = 0; i < stack.Length; i++)
        {
            if (stack[i] == null)
            {
                stack[i] = croassant_;
            }
        }
        curCapacity++;

    }

}
