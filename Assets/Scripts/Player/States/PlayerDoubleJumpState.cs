using UnityEngine;

public class PlayerDoubleJumpState : PlayerJumpState
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        player.ConsumeAirJump();
        player.Anim.ResetTrigger("DoubleJump");
    }
}