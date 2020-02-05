using UnityEngine;
using UnityEngine.UI;

public class SliderBehaviour : MonoBehaviour
{
    [SerializeField] private GameEvent gameOver;
    [SerializeField] private ScoreCounter scoreData;
    [SerializeField] private Slider slider;

    private void Awake()
    {
        scoreData.Score = 0;
        slider.value = scoreData.Score;
        slider.maxValue = scoreData.MaxScore;
    }

    public void OnScoreChanged()
    {
        slider.value = scoreData.Score;
        if (scoreData.Score >= scoreData.MaxScore)
            gameOver.Raise();
    }
}