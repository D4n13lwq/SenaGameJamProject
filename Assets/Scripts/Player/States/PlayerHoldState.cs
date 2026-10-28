using UnityEngine;

public class PlayerHoldState : BasePlayerState
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        if (player.IsHanging)
            player.KinematicTarget = player.ActiveHangZone.HangPositionAt(player.KinematicTarget.x);
    }
}