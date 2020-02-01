using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data", order = 55)]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private float attackPower;
    [SerializeField]
    private float modifiedAttackPower;
    public float AttackPower
    {
        get
        {
            return modifiedAttackPower;
        }

        set
        {
            modifiedAttackPower = value;
        }
    }

    public float StartAttackPower
    {
        get
        {
            return attackPower;
        }
    }
    
}
