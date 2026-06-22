using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace ProjectConnections.SceneUI
{
    public class AudioButtonController : MonoBehaviour
    {
        [SerializeField, Required] AudioMixer audioMixer;
        [SerializeField, Required] Sprite activeButton;
        [SerializeField, Required] Sprite inactiveButton;
        [SerializeField, Required] Button button;

        bool isOn = true;

        void Start()
        {
            float value;
            audioMixer.GetFloat("MyVolume", out value);
            if (value == -15) isOn = true;
            else isOn = false;
            button.onClick.AddListener(OnClickAudio);
        }

        void OnClickAudio()
        {
            isOn = !isOn;
            ToggleButton();
        }

        void ToggleButton()
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
}
