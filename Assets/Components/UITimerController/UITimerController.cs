using TMPro;
using UnityEngine;
using Services.GameEventService;

public class UITimerController : MonoBehaviour
{
    [SerializeField] private TMP_Text _timerText;

    private bool _isGameOver;

    private void Start()
    {
        GameEventService.OnTimerTick += SetTimer;
        GameEventService.OnGameOverState += HandleGameOver;
    }

    private void OnDestroy()
    {
        GameEventService.OnTimerTick -= SetTimer;
        GameEventService.OnGameOverState -= HandleGameOver;
    }
    
    private void HandleGameOver(bool enterState)
    {
        _isGameOver = enterState;
    }

    private void SetTimer(float timer)
    {
        if (_isGameOver)
            return;
        _timerText.text = timer.ToString("0");
    }
}
