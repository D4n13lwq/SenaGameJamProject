using UnityEngine;

public abstract class PlayerHangMoveState : BasePlayerState
{
    [SerializeField] float hangSpeed = 1.5f;

    protected abstract float Direction { get; }

    private float x;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        x = player.KinematicTarget.x;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!player.IsHanging) return;

        float input = Mathf.Max(0f, player.MoveInput.x * Direction);
        x += Direction * input * hangSpeed * Time.deltaTime;

        Vector3 target = player.ActiveHangZone.HangPositionAt(x);
        x = target.x;
        player.KinematicTarget = target;
    }
}