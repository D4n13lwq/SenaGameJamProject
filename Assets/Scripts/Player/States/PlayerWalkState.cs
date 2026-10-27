using UnityEngine;

public class PlayerWalkState : BasePlayerState
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 720f;

    private Vector2 moveInput;

    public override void OnStateUpdate(Animator animator,AnimatorStateInfo stateInfo,int layerIndex)
    {
        moveInput = player.PlayerInput.actions.FindActionMap("Player").FindAction("Move").ReadValue<Vector2>();
    }

    public override void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 velocity = player.Rb.linearVelocity;

        velocity.x = moveInput.x * speed;
        velocity.z = 0f;

        player.Rb.linearVelocity = velocity;

        if (Mathf.Abs(moveInput.x) > 0.01f)
        {
            float targetAngle = moveInput.x > 0f ? 90f : -90f;

            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

            player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 velocity = player.Rb.linearVelocity;

        velocity.x = 0f;
        velocity.z = 0f;

        player.Rb.linearVelocity = velocity;
        moveInput = Vector2.zero;
    }
}