using UnityEngine;

public class Joystick : MonoBehaviour
{

    // TODO : ��������
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Canvas canvas;

    private Camera cameraMain;
    private void Awake()
    {
        // ���߿� Awake���� ������ �� 
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
