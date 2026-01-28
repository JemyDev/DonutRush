using UnityEngine;
using Services.SaveService;
using Services.SceneLoaderService;

public class UIMainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        // Save total runs
        SaveDataService.UpdateRunCount();
        SceneLoaderService.LoadLevel();
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
