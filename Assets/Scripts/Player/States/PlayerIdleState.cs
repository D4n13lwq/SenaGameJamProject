using UnityEngine;

public class PlayerIdleState : BasePlayerState
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        player.Rb.linearVelocity = Vector2.zero;
    }
}
