using System.Collections;
using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    public bool IsEnter { get { return isEnter; } }           
    private bool isEnter = default;
    public PlayerController player = default;
    
    private Vector3 scaleOffset = default;
    private Vector3 originScale = default;

    void Awake()
    {
        Init();
    }

    void Init()
    {
        isEnter = false;
        scaleOffset = new Vector3(0.5f, 0f, 0.5f);
        originScale = this.transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(AddScale());
            player = other.GetComponent<PlayerController>();    
            isEnter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            isEnter = false;
            StartCoroutine(ReduceScale());
        }
    }


    IEnumerator AddScale()
    {
        float ratio = 0f;
        
        Vector3 targetScale = transform.localScale + scaleOffset;
        while ((transform.localScale.x) < targetScale.x)
        {
            ratio += 0.1f;
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale,ratio);
            yield return null;
        }
    }

    IEnumerator ReduceScale()
    {
        float ratio = 0f;

        Vector3 targetScale = originScale;
        while ((transform.localScale.x) > originScale.x)
        {
            ratio += 0.1f;
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, ratio);
            yield return null;
        }
    }

}
