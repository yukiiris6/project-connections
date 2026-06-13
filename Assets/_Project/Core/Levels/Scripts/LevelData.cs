using UnityEngine;

public class LevelData
{
    public string fileName;
    public string levelDisplayName;
    public bool isLocked = true;
    public bool isFinished = false;
    public bool isMenu;
    public AudioClip levelMusic;

    public LevelData(LevelDataSO levelDataSO)
    {
        fileName = levelDataSO.sceneName;
        levelDisplayName = levelDataSO.levelDisplayName;
        isMenu = levelDataSO.isMenu;
        levelMusic = levelDataSO.levelMusic;
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