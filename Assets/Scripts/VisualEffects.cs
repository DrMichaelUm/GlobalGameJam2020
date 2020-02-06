using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffects : MonoBehaviour
{
    [SerializeField] private ItemAffect _item;
    [SerializeField] private ParticleSystem destroyParticles;
    [SerializeField] private ParticleSystem repairParticles;

    private bool wasDestroyedLastTime = false;
    private bool wasNotDamagedLastTime = true;
    
    public void PlayDestroy()
    {
        if (!IsDestroyed())
        {
            wasDestroyedLastTime = false;
            destroyParticles.Play();
        }
        else if (!wasDestroyedLastTime)
        {
            destroyParticles.Play();
            wasDestroyedLastTime = true;
        }
    }

    public void PlayRepair()
    {
        if (!IsNotDamaged())
        {
            wasNotDamagedLastTime = false;
            repairParticles.Stop();
            repairParticles.Play();
        }
        else if (!wasNotDamagedLastTime)
        {
            repairParticles.Stop();
            repairParticles.Play();
            wasNotDamagedLastTime = true;
        }
    }

    private bool IsDestroyed()
    {
        return _item.destroyed;
    }

    private bool IsNotDamaged()
    {
        return !_item.damaged;
    }
}
