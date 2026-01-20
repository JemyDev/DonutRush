using TMPro;
using UnityEngine;

public class UITimerController : MonoBehaviour
{
    [SerializeField] private float _totalTime = 60f;
    [SerializeField] private TMP_Text _timerText;

    private bool _isGameOver = false;

    private void Start()
    {
        GameEventService.OnOrderCompleted += ResetTimer;
        GameEventService.OnGameOver += HandleGameOver;
    }

    private void OnDestroy()
    {
        GameEventService.OnOrderCompleted -= ResetTimer;
        GameEventService.OnGameOver -= HandleGameOver;
    }
    
    private void HandleGameOver()
    {
        _isGameOver = true;
    }

    private void Update()
    {
        if (!(_totalTime > 0) || _isGameOver) return;
        _totalTime -= Time.deltaTime;
        var timeToDisplay = Mathf.CeilToInt(_totalTime);
        _timerText.text = timeToDisplay.ToString();
    }
    
    private void ResetTimer(int obj)
    {
        _totalTime = 60f;
    }
}
