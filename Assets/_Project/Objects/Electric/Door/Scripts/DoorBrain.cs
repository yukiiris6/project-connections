using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Rendering.Universal;

namespace ProjectConnections.Electric
{
    public class DoorBrain : MonoBehaviour
    {
        [SerializeField, Required] ObjectEnergizer objectEnergizer;
        [SerializeField, Required] DoorSoundPlayer soundPlayer;
        [SerializeField, Required] DoorPresenter presenter;

        void OnEnable()
        {
            UpdateState(objectEnergizer.IsOn);
            objectEnergizer.OnChangedState += UpdateState;
        }

        void OnDisable()
        {
            objectEnergizer.OnChangedState -= UpdateState;
        }

        public void UpdateState(bool hasEnergy)
        {
            if (hasEnergy) soundPlayer.PlayOpeningSFX();
            else soundPlayer.PlayClosingSFX();
            presenter.UpdateState(hasEnergy);
        }
    }
}
