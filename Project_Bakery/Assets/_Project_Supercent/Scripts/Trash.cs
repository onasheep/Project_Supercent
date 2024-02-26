using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Instantiate(ResourceManager.vfx["VFX_Clean"], this.transform.position,Quaternion.identity);
            SoundManager.Instance.OnPlayClip(RDefine.SFX_TRASH);
            Destroy(gameObject);
        }
    }
}
