using System;
using System.Collections;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeOfGame = 30f;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameEvent gameEnded;
    [ReadOnly] public float remainingTime;
    
    public void OnGameStarted()
    {
        remainingTime = timeOfGame;
        StartCoroutine ("Countdown");
    }

    private IEnumerator Countdown()
    {
        while (remainingTime > 0)
        {
            remainingTime -= 1;
            SetTimerText();

            yield return new WaitForSeconds (1);
        }
        gameEnded.Raise();
    }

    private void SetTimerText()
    {
        string text = String.Format ("{0:d2}:{1:d2}", (int)(remainingTime / 60), (int)(remainingTime % 60));
        timerText.text = text;
    }
}
