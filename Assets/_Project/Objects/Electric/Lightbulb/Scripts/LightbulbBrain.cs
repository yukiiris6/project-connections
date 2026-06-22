using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Electric
{
    public class LightbulbBrain : MonoBehaviour
    {
        [SerializeField, Required] ObjectEnergizer objectEnergizer;
        [SerializeField, Required] LightbulbPresenter presenter;

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
            presenter.UpdateState(hasEnergy);
        }
    }
}
