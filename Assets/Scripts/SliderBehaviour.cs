using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBehaviour : MonoBehaviour
{
    [SerializeField] private ScoreCounter scoreData;
    private Slider _slider;

    private void Awake()
    {
        scoreData.Score = 0;
        _slider = GetComponent<Slider>();
    }

    public void OnScoreChanged()
    {
        _slider.value = scoreData.Score;
    }
}
