using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXCallback : MonoBehaviour
{
    void Awake()
    {
        var _system = GetComponent<ParticleSystem>().main;
        _system.stopAction = ParticleSystemStopAction.Callback;
    }

    private void OnParticleSystemStopped()
    {
        Destroy(gameObject);
    }
}
