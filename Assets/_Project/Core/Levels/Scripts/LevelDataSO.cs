using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataSO", menuName = "Scriptable Objects/LevelDataSO")]
public class LevelDataSO : ScriptableObject
{
    public string sceneName;
    public string levelDisplayName;
    public bool isMenu;
    public AudioClip levelMusic;
}
