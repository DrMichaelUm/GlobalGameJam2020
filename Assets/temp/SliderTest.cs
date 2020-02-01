using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class SliderTest : MonoBehaviour
{
    public GameEvent destroy;
    public GameEvent repair;
    public ScoreCounter score;
    
    [Button]
    public void Destroying()
    {
        Debug.Log ("Destroy event");
        score.Score += 10;
        destroy.Raise();
    }
    [Button]
    public void Repairing()
    {
        Debug.Log ("Repair event");
        score.Score -= 10;
        repair.Raise();
    }
}
