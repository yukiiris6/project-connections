using UnityEngine;

public interface LocomotionContext
{
    Jumper Jumper { get; }
    GroundValidator GroundValidator { get; }
    PlayerSoundPlayer SoundPlayer { get; }
    LocomotionPresenter Presenter { get; }
    void SetState(LocomotionState newState);
}