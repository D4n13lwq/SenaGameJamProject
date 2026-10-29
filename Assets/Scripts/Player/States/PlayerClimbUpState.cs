using UnityEngine;

public class PlayerClimbUpState : BasePlayerState
{
    Vector3 start;
    Vector3 end;
    bool finished;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        finished = !player.IsHanging;
        if (finished) return;

        start = player.KinematicTarget;
        end = new Vector3(start.x, player.StandPosition.y, player.StandPosition.z);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (finished) return;

        float raw = stateInfo.normalizedTime;
        float t = Mathf.SmoothStep(0f, 1f, Mathf.Clamp01(raw));
        player.KinematicTarget = Vector3.Lerp(start, end, t);

        if (raw >= 1f) Finish();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        if (!finished) Finish();
    }

    private void Finish()
    {
        player.KinematicTarget = end;
        player.EndHang();
        finished = true;
    }
}