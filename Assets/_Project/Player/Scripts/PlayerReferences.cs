using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.UIShared;

namespace ProjectConnections.Player
{
    public class PlayerReferences : MonoBehaviour
    {
        [field: SerializeField] public PlayerAnimationBrain AnimationBrain;
        [field: SerializeField] public PlayerInputMapper InputMapper;
        [field: SerializeField] public PlayerMovement Movement;
    }
}
