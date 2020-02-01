using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSkill : MonoBehaviour, IPlayable
{
    [SerializeField] private GameEvent OnRepairGunSkillCasted;

    private void Update()
    {
        if (Input.GetMouseButton(0))
            Skill();
    }
    public void Skill()
    {
        OnRepairGunSkillCasted.Raise();
    }
}
