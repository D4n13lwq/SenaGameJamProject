using UnityEngine;

public class PlayerEntranceState : BasePlayerState
{
    [SerializeField] float walkSpeed = 2f;

    static readonly Quaternion FacingCamera = Quaternion.Euler(0f, 180f, 0f);

    Vector3 start;
    Vector3 end;
    float duration;
    float timer;
    bool active;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        Transform door = player.EntranceDoor;
        active = door != null;

        if (!active)
        {
            animator.SetTrigger("EntranceDone");
            return;
        }

        end = player.Rb.position;
        start = door.position;

        duration = Vector3.Distance(start, end) / Mathf.Max(0.01f, walkSpeed);
        timer = 0f;

        player.BeginCutscene(start, FacingCamera);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!active) return;

        timer += Time.deltaTime;
        float t = duration > 0f ? Mathf.Clamp01(timer / duration) : 1f;

        player.KinematicTarget = Vector3.Lerp(start, end, t);
        player.KinematicRotation = FacingCamera;

        if (t >= 1f)
        {
            active = false;
            player.EndCutscene();
            animator.SetTrigger("EntranceDone");
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        if (active)
        {
            active = false;
            player.KinematicTarget = end;
            player.EndCutscene();
        }
        animator.ResetTrigger("EntranceDone");
    }
}
