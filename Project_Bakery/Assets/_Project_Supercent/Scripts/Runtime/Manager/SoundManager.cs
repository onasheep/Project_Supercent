using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance = default;
    public static SoundManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private AudioSource source = default;    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

        source = GetComponent<AudioSource>();
    }

   
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayClip(string clipName)
    {
        source.PlayOneShot(ResourceManager.sfx[clipName]);
    }
}
