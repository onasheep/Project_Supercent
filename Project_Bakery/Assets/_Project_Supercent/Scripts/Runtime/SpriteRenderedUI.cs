using UnityEngine;

public class SpriteRenderedUI : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.transform.forward = Camera.main.transform.forward;
    }
}
