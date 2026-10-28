using UnityEngine;

public class PlayerWalkState : BasePlayerState
{
    [SerializeField] float speed = 5f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        player.MovementEnabled = true;
        player.HorizontalSpeed = speed;
    }
}