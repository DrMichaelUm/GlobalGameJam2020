using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class TimerTest : MonoBehaviour
{
    public GameEvent gameStart;
    
    [Button]
    public void StartGame()
    {
        gameStart.Raise();
    }
}
