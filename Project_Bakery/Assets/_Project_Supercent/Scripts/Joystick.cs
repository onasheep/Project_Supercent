using UnityEngine;

public class Joystick : MonoBehaviour
{

    // TODO : 변경하자
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Canvas canvas;

    private Camera cameraMain;
    private void Awake()
    {
        // 나중에 Awake에서 가져올 것 
        //inputManager = ;
        cameraMain = Camera.main;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += Move;
    }

    private void OnDisable()
    {
        inputManager.OnEndTouch -= Move;
    }

    public void Move(Vector2 screenPosition, float time)
    {
        Vector3 screenCoordinates = new Vector3(screenPosition.x, screenPosition.y, cameraMain.nearClipPlane);
        Vector3 worldCoordinates = cameraMain.ScreenToWorldPoint(screenCoordinates);
        worldCoordinates.z = 0;
        transform.position = worldCoordinates;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
