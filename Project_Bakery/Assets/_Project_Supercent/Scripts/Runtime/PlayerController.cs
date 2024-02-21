using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    private Vector2 inputVector = default;
    private Animator animator = default;
    private Rigidbody rigid = default;

    [SerializeField]
    private float moveSpeed = 10f;


    public GameObject spawnPoint = default;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    // Update is called once per frame
    void Update()
    {

        animator.SetFloat("moveSpeed", inputVector.magnitude);

    }

    private void MovePlayer()
    {
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
