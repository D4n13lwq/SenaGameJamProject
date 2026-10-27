using UnityEngine;

public class BasePlayerState : StateMachineBehaviour
{
    protected Player player;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<Player>();
    }
}