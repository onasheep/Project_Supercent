using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    private Touch firstTouch = default;
    private Vector2 touchPos = default;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("FT" + firstTouch);
        Debug.Log("TP" + touchPos);
    }

    public void OnFirstTouch(InputAction.CallbackContext context)
    {
        firstTouch = context.ReadValue<Touch>();
    }

    public void OnTouchPosition(InputAction.CallbackContext context)
    {
        touchPos = context.ReadValue<Vector2>();
    }
}
