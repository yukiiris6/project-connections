using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Rendering.Universal;

namespace ProjectConnections.Electric
{
    public class ElevatorBrain : MonoBehaviour
    {
        [SerializeField, Required] ObjectEnergizer objectEnergizer;
        [SerializeField, Required] ElevatorController controller;
        [SerializeField, Required] ElevatorPresenter presenter;
        [SerializeField, Required] ElevatorDockPresenter dockPresenter;

        void OnEnable()
        {
            UpdateState(objectEnergizer.IsOn);
            objectEnergizer.OnChangedState += UpdateState;
        }

        void OnDisable()
        {
            objectEnergizer.OnChangedState -= UpdateState;
        }

        void UpdateState(bool hasEnergy)
        {
            controller.UpdateState(hasEnergy);
            presenter.UpdateState(hasEnergy);
            dockPresenter.UpdateState(hasEnergy);
        }
    }
}
