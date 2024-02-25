using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    private Vector2 inputVector = default;
    private Animator animator = default;
    private Rigidbody rigid = default;

    private Stack<GameObject> stackers = new Stack<GameObject>();
    
    [SerializeField]
    private float moveSpeed = 10f;

    private enum AnimType
    {
        NONE = -1, DEFAULT, STACK
    }
    private AnimType animType = AnimType.NONE;

    public ObjectStacker objectStacker = default;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    void Init()
    {

    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    // Update is called once per frame
    void Update()
    {
        SwitchAnimation(objectStacker.IsStack);
    }

    private AnimType SetAnimType(bool isStack_)
    {
        AnimType type = default;
        type = isStack_ == true ? AnimType.STACK : AnimType.DEFAULT;

        return type;
    }
    private void SwitchAnimation(bool isStack_)
    {
        if(animType == SetAnimType(isStack_)) { return; }
        
        switch(SetAnimType(isStack_))
        {
            case AnimType.DEFAULT:
                animator.SetBool("isStack", false);
                break;
            case AnimType.STACK:
                animator.SetBool("isStack", true);
                break;
            default:
                break;

        }
    }
    private void MovePlayer()
    {
        animator.SetFloat("moveSpeed", inputVector.magnitude);

        if (inputVector == Vector2.zero)
        {
            rigid.velocity = Vector3.zero;
            return;
        }
        Vector3 move = new Vector3(inputVector.x, 0f, inputVector.y);
        move.Normalize();

        rigid.velocity = move * moveSpeed;
        rigid.transform.forward = move;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
    }
}
