using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace ProjectConnections.SceneUI
{
    public class InteractionGUIPresenter : MonoBehaviour
    {
        [SerializeField, Required] TMP_Text interactionTMP;

        public void ToggleGUI(bool shouldEnable)
        {
            interactionTMP.gameObject.SetActive(shouldEnable);
        }

        public void SetLabel(string newLabel)
        {
            interactionTMP.text = "[E]" + newLabel;
        }
    }
}