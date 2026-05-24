using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AudioButtonController : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Sprite activeButton;
    [SerializeField] Sprite inactiveButton;

    Button button;
    bool isOn = true;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void Start()
    {
        float value;
        audioMixer.GetFloat("MyVolume", out value);
        if (value == -15)
        {
            isOn = true;
        }
        else
        {
            isOn = false;
        }
        SetActive();
    }

    public void OnClickAudio()
    {
        isOn = !isOn;
        SetActive();
    }

    public void SetActive()
    {
        if (isOn)
        {
            audioMixer.SetFloat("MyVolume", -15);
            button.image.sprite = activeButton;
        }
        else
        {
            audioMixer.SetFloat("MyVolume", -80);
            button.image.sprite = inactiveButton;
        }
    }
}
