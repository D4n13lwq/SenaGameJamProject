using UnityEngine;

public class PlayerHoldState : BasePlayerState
{
    [SerializeField] float hangSpeed = 1.5f;

    float x;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        x = player.KinematicTarget.x;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!player.IsHanging) return;

        x += player.MoveInput.x * hangSpeed * Time.deltaTime;

        Vector3 target = player.ActiveHangZone.HangPositionAt(x);
        x = target.x;
        player.KinematicTarget = target;
    }
}