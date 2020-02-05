using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSkill : MonoBehaviour, ISkillable
{
    [SerializeField] private GameEvent OnDestroySkillCasted;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Skill();
        }
    }

    public void Skill()
    {
        //TODO Партикл и звук волны
        Debug.Log("Boom!");
        OnDestroySkillCasted.Raise();
    }
}
