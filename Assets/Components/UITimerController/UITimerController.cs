using System;
using TMPro;
using UnityEngine;

public class UITimerController : MonoBehaviour
{
    [SerializeField] private float _totalTime = 60f;
    [SerializeField] private TMP_Text _timerText;

    private void Start()
    {
        GameEventSystem.OnOrderCompleted += ResetTimer;
    }

    private void OnDestroy()
    {
        GameEventSystem.OnOrderCompleted -= ResetTimer;
    }

    private void Update()
    {
        if (!(_totalTime > 0)) return;
        _totalTime -= Time.deltaTime;
        var timeToDisplay = Mathf.CeilToInt(_totalTime);
        _timerText.text = timeToDisplay.ToString();
    }
    
    private void ResetTimer(int obj)
    {
        _totalTime = 60f;
    }
}
