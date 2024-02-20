using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller = default;
    private Vector2 inputVector = default;
    private Animator animator = default;
    private Rigidbody rigid = default;

    [SerializeField]
    private float moveSpeed = 10f;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(inputVector == Vector2.zero) 
        { 
            rigid.velocity = Vector3.zero;
            return;
        }
        Vector3 move = new Vector3(inputVector.x, 0f, inputVector.y);
        move.Normalize();
       //controller.Move(move * Time.deltaTime * moveSpeed);

        //controller.transform.forward = move;
        //rigid.AddForce(move * moveSpeed, ForceMode.Impulse);
        rigid.velocity = move * moveSpeed;
        rigid.transform.forward = move;
        //this.transform.TransformDirection(move);
    }
    // Update is called once per frame
    void Update()
    {

        animator.SetFloat("moveSpeed", inputVector.magnitude);

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
    }
}
