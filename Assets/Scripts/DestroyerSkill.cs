using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerSkill : MonoBehaviour, IPlayable
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
        OnDestroySkillCasted.Raise();
    }
}
