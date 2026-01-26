using System;
using Services.GameEventService;
using TMPro;
using UnityEngine;

namespace Components.CountdownController
{
    public class UICountdownController : MonoBehaviour
    {
        [SerializeField] private GameObject _countdownOverlay;
        [SerializeField] private TMP_Text _countdownText;

        public void Awake()
        {
            GameEventService.OnCountdownState += HandleCountdownState;
            GameEventService.OnCountdownTick += SetCountdown;
        }

        private void OnDestroy()
        {
            GameEventService.OnCountdownState -= HandleCountdownState;
            GameEventService.OnCountdownTick -= SetCountdown;
        }
        
        private void HandleCountdownState(bool enterState)
        {
            _countdownOverlay.SetActive(enterState);
        }

        private void SetCountdown(float countdown)
        {
            _countdownText.text = countdown.ToString("0");

            if (countdown < 1)
            {
                _countdownText.text = "GO!";
            }
        }
    }
}