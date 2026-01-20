using System;
using UnityEngine;

public class UIGameOverOverlayController : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverUI;
    
    private void Start()
    {
        GameEventService.OnGameOver += HandleGameOver;
    }

    private void OnDestroy()
    {
        GameEventService.OnGameOver -= HandleGameOver;
    }
    
    private void HandleGameOver()
    {
        _gameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        SceneLoaderService.LoadLevel();
    }

    public void BackToMainMenu()
    {
        SceneLoaderService.LoadMainMenu();
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
