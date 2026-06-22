using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Rendering.Universal;

namespace ProjectConnections.Electric
{
    public class ElectricObjectLight : MonoBehaviour
    {
        [SerializeField, Required] Light2D light2D;
        [SerializeField, Required] Color offLightColor = new Color32(215, 40, 40, 255);
        [SerializeField, Required] Color onLightColor = new Color32(35, 215, 215, 255);

        public void UpdateState(bool isOn)
        {
            light2D.color = isOn ? onLightColor : offLightColor;
        }
    }
}
