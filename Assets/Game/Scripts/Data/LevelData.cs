public class LevelData
{
    public string fileName;
    public string levelDisplayName;
    public bool isLocked = true;
    public bool isFinished = false;

    public LevelData(LevelDataSO levelDataSO)
    {
        fileName = levelDataSO.fileName;
        levelDisplayName = levelDataSO.levelDisplayName;
        isLocked = levelDataSO.isLocked;
    }

    public void SetIsLocked(bool value)
    {
        isLocked = value;
    }

    public void SetIsFinished(bool value)
    {
        isFinished = value;
    }
}