using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "LevelDataSO", menuName = "Scriptable Objects/LevelDataSO")]
public class LevelDataSO : ScriptableObject
{
    public string SceneName;
    public string DisplayName;
    public LevelType Type;
    public AudioClip Music;
}
