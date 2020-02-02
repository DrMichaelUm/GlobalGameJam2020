using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data", order = 55)]
public class PlayerData : ScriptableObject
{
    [SerializeField] private float attackPower;
    [SerializeField] private float knockStrength;

    public float AttackPower
    {
        get
        {
            return attackPower;
        }

        set
        {
            attackPower = value;
        }
    }

    public float Strength
    {
        get
        {
            return knockStrength;
        }
    }
    
}
