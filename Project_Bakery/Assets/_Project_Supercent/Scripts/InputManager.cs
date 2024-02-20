using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public delegate void StartTouchEvent(Vector2 position, float time);
    public event StartTouchEvent OnStartTouch;
    public delegate void EndTouchEvent(Vector2 position, float time);
    public event EndTouchEvent OnEndTouch;


    public PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerControls.Player.TouchInput.started += ctx => StartTouch(ctx);
        playerControls.Player.TouchInput.canceled += ctx => EndTouch(ctx);
    }

    void StartTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch Started" + playerControls.Player.TouchPosition.ReadValue<Vector2>());
        if (OnStartTouch != null)
        {
            OnStartTouch(playerControls.Player.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
        }
    }

    void EndTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch ended" + playerControls.Player.TouchPosition.ReadValue<Vector2>());
        if (OnEndTouch != null)
        {
            OnEndTouch(playerControls.Player.TouchPosition.ReadValue<Vector2>(), (float)context.time);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
