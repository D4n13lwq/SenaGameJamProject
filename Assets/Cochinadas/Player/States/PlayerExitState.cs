using UnityEngine;

public class PlayerExitState : BasePlayerState
{
    [SerializeField] float walkSpeed = 2f;

    static readonly Quaternion FacingDoor = Quaternion.Euler(0f, 0f, 0f);

    LevelExit exit;
    Vector3 start;
    Vector3 end;
    float duration;
    float timer;
    bool active;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        animator.ResetTrigger("ExitLevel");

        exit = player.CurrentExit;
        active = exit != null;
        if (!active) return;

        end = exit.DoorPoint.position;

        Vector3 current = player.Rb.position;
        start = new Vector3(end.x, current.y, current.z);

        duration = Vector3.Distance(start, end) / Mathf.Max(0.01f, walkSpeed);
        timer = 0f;

        player.BeginCutscene(start, FacingDoor);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!active) return;

        timer += Time.deltaTime;
        float t = duration > 0f ? Mathf.Clamp01(timer / duration) : 1f;

        player.KinematicTarget = Vector3.Lerp(start, end, t);
        player.KinematicRotation = FacingDoor;

        if (t >= 1f)
        {
            active = false;
            exit.Complete(player);
        }
    }
}
