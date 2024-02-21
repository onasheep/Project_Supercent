using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStacker : MonoBehaviour
{

    // 추후 확장성을 고려해서 Stack이 가능한 오브젝트들의 상위 클래스 타입으로 가져오기
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
