using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Score Counter", menuName = "Score Counter", order = 54)]
public class ScoreCounter : ScriptableObject
{
    [SerializeField]
    private int score;

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            if (value <= 0)
                score = 0;
            else
                score = value;
        }
    }

    [SerializeField] 
    private int maxScore;

    public int MaxScore
    {
        get
        {
            return maxScore;
        }
        set
        {
            if (value > 0)
                maxScore = value;
        }
    }
}
