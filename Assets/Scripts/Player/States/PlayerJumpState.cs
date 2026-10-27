using UnityEngine;

public class PlayerJumpState : BasePlayerState
{
    [Tooltip("Velocidad vertical que se asigna directamente a rb.linearVelocity.y al entrar.")]
    [SerializeField] protected float jumpForce = 8f;
    [SerializeField] protected float speed = 5f;

    [SerializeField, Range(0f, 1f)] float jumpCutMultiplier = 0.35f;

    bool jumpCut;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        player.MovementEnabled = true;
        player.HorizontalSpeed = speed;
        player.Anim.ResetTrigger("Jump");

        Vector3 velocity = player.Rb.linearVelocity;
        velocity.y = jumpForce;
        player.Rb.linearVelocity = velocity;

        jumpCut = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (jumpCut) return;

        Vector3 velocity = player.Rb.linearVelocity;
        if (!player.JumpHeld && velocity.y > 0f)
        {
            velocity.y *= jumpCutMultiplier;
            player.Rb.linearVelocity = velocity;
            jumpCut = true;
        }
    }
}