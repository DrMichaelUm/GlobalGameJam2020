using UnityEngine;
using UnityEngine.UI;

public class SliderBehaviour : MonoBehaviour
{
    [SerializeField] private GameEvent gameOver;
    [SerializeField] private ScoreCounter scoreData;
    private Slider _slider;

    private void Awake()
    {
        scoreData.Score = 0;
        _slider = GetComponent<Slider>();
    }

    public void OnGameStarted()
    {
        scoreData.Score = 0;
        _slider.value = scoreData.Score;
        _slider.maxValue = scoreData.MaxScore;
    }

    public void OnScoreChanged()
    {
        _slider.value = scoreData.Score;
        if (scoreData.Score >= scoreData.MaxScore)
            gameOver.Raise();
    }
}
