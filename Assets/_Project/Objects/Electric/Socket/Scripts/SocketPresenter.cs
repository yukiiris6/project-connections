using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Electric
{
    public class SocketPresenter : MonoBehaviour
    {
        [SerializeField, Required] LineRenderer lineRenderer;

        float radius;

        public void ToggleRangeCircle(bool shouldShow)
        {
            lineRenderer.enabled = shouldShow;
        }
    }
}