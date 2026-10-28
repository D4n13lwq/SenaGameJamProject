using UnityEngine;

public class PlayerClimbDownState : BasePlayerState
{
    [SerializeField, Range(0f, 0.9f)] float turnPortion = 0.25f;

    static readonly Quaternion BackToCamera = Quaternion.Euler(0f, 0f, 0f);

    Vector3 start;
    Vector3 end;
    Quaternion startRotation;
    bool active;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        HangZone zone = player.CurrentHangZone;
        active = zone != null;
        if (!active)
        {
            return;
        }

        player.BeginHang(zone);

        start = player.Rb.position;
        startRotation = player.Rb.rotation;

        end = zone.HangPositionAt(start.x);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!active) return;

        float n = Mathf.Clamp01(stateInfo.normalizedTime);

        if (turnPortion > 0f && n < turnPortion)
        {
            float turnT = Mathf.SmoothStep(0f, 1f, n / turnPortion);
            player.KinematicRotation = Quaternion.Slerp(startRotation, BackToCamera, turnT);
            player.KinematicTarget = start;
        }
        else
        {
            player.KinematicRotation = BackToCamera;

            float descentT = turnPortion >= 1f ? 1f : (n - turnPortion) / (1f - turnPortion);
            float t = Mathf.SmoothStep(0f, 1f, descentT);
            player.KinematicTarget = Vector3.Lerp(start, end, t);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!active) return;
        player.KinematicTarget = end;
        player.KinematicRotation = BackToCamera;
    }
}