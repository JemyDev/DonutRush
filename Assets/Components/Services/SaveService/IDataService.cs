namespace Services.SaveService
{
    public interface IDataService
    {
        SaveData GetSaveData();
        void Save(SaveData saveData);
    }
}
