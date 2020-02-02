using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectsPlay : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    
    public void OnEffectCalled()
    {
        _particleSystem.Stop();
        _particleSystem.Play();
    }
}