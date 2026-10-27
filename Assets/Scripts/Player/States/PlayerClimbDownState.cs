using UnityEngine;

public class PlayerClimbDownState : BasePlayerState
{
    Vector3 start;
    Vector3 end;
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
        end = zone.HangPositionAt(start.x);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!active) return;

        float t = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(stateInfo.normalizedTime));
        player.KinematicTarget = Vector3.Lerp(start, end, t);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (active) player.KinematicTarget = end;
    }
}