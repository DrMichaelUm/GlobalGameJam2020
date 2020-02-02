using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSkill : MonoBehaviour
{
    [SerializeField] private GameEvent OnRepairGunSkillCasted;
    [SerializeField] private ParticleSystem trailsGunParticles;
    
    private void Update()
    {
        if (Input.GetMouseButton(0))
            Skill();
        
        else if (Input.GetMouseButtonUp (0))
            trailsGunParticles.Stop();
    }
    public void Skill()
    {
        OnRepairGunSkillCasted.Raise();
        trailsGunParticles.Play();
    }
}
