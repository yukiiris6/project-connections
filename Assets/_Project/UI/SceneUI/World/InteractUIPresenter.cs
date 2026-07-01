using ProjectConnections.Player;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace ProjectConnections.SceneUI
{
    public class InteractUIPresenter : MonoBehaviour
    {
        [SerializeField, Required] GameObject container;
        [SerializeField, Required] TMP_Text interactionTMP;
        [SerializeField, Required] InteractableController interactableController;

        void OnEnable()
        {
            interactableController.OnUICalled += UpdateUI;
        }

        void OnDisable()
        {
            interactableController.OnUICalled -= UpdateUI;
        }

        void UpdateUI(bool shouldEnable, string newLabel)
        {
            ToggleUI(shouldEnable);
            if (shouldEnable) SetLabel(newLabel);
        }

        public void ToggleUI(bool shouldEnable)
        {
            container.SetActive(shouldEnable);
        }

        public void SetLabel(string newLabel)
        {
            interactionTMP.text = newLabel;
        }
    }
}