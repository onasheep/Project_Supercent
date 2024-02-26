using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class ObjectStacker : MonoBehaviour
{

    public Croassant[] stack = default;

    private GameObject maxText = default;

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
        maxText = gameObject.FindChildObj("MaxText");
        maxText.SetActive(false);
        isFull = false;      
    }
    // Update is called once per frame
    void Update()
    {

        CheckArray();
        SetMaxText();
    }

    private void SetMaxText()
    {
        if(curCapacity < maxCapacity) { return; }

        maxText.SetActive(true);
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
        // юс╫ц
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
