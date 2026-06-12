using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectConnections.Electric
{
    public class SocketPresenter : MonoBehaviour
    {
        [SerializeField] LineRenderer lineRenderer;

        float radius;

        public void ToggleRangeCircle(bool shouldShow)
        {
            lineRenderer.enabled = shouldShow;
        }
    }
}