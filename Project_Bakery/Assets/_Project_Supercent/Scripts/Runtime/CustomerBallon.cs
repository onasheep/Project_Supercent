using UnityEngine;

public class CustomerBallon : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(Camera.main.transform);
    }
}
