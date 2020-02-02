using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GunSkill : MonoBehaviour
{
    [SerializeField] private GameEvent OnRepairGunSkillCasted;

    [SerializeField] private UnityEvent OnShooted;
    [SerializeField] private UnityEvent OnStoppedShooting;
    
    private void Update()
    {
        if (Input.GetMouseButton(0))
            Skill();
        
        else if (Input.GetMouseButtonUp (0))
            OnStoppedShooting.Invoke();
    }
    public void Skill()
    {
        OnRepairGunSkillCasted.Raise();
        OnShooted.Invoke();
    }
}
