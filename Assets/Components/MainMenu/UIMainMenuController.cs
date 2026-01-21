using UnityEngine;
using Services.SaveService;
using Services.SceneLoaderService;

public class UIMainMenuController : MonoBehaviour, IDataService
{
    public void PlayGame()
    {
        // Save total ingredients collected
        var saveData = GetSaveData();
        saveData.RunCount++;
        Save(saveData);

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

    public SaveData GetSaveData()
    {
        if (!SaveService.TryLoad(out var saveData))
        {
            saveData = new SaveData();
        }

        return saveData;
    }

    public void Save(SaveData saveData)
    {
        SaveService.Save(saveData);
    }
}
