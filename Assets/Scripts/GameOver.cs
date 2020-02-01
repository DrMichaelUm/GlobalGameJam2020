using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private ScoreCounter score;
    [SerializeField] private Timer timer;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI mainMessage;
    [SerializeField] private TextMeshProUGUI destroyerMessage;
    [SerializeField] private TextMeshProUGUI repairerMessage;
    
    public void OnGameEnded()
    {
        if (timer.remainingTime <= 0)
        {
            mainMessage.text = "The Library is saved";
            destroyerMessage.text = "You lost";
            repairerMessage.text = "You won";
        }
        else if (score.Score >= score.MaxScore)
        {
            mainMessage.text = "The Library is destroyed";
            destroyerMessage.text = "You won";
            repairerMessage.text = "You lost";
        }
        gameOverPanel.SetActive (true);
    }
}
