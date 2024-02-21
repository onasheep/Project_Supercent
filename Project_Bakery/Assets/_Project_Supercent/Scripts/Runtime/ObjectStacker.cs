using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStacker : MonoBehaviour
{

    // ���� Ȯ�强�� ����ؼ� Stack�� ������ ������Ʈ���� ���� Ŭ���� Ÿ������ ��������
    Stack<Croassant> stack;

    int maxCapacity = 8;

    bool isFull = default;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        stack = new Stack<Croassant>();
        isFull = false;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void AddToStack(Croassant croassant_)
    {
        stack.Push(croassant_);
    }

    bool isFullStack()
    {

        bool isFull = stack.Count >= maxCapacity? true : false;

        return isFull;
    }
}
