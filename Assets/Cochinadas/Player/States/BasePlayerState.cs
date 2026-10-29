using UnityEngine;

public class BasePlayerState : StateMachineBehaviour
{
    protected Player player;
    protected PlayerAudio playerAudio;

    [SerializeField] AudioClip stateSound;
    [SerializeField] bool loopSound;
    [SerializeField, Range(0f, 1f)] float soundVolume = 1f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = animator.GetComponent<Player>();

        if (playerAudio == null && !animator.TryGetComponent(out playerAudio))
            playerAudio = animator.gameObject.AddComponent<PlayerAudio>();

        if (stateSound == null) return;

        if (loopSound)
            playerAudio.PlayLoop(stateSound, soundVolume, this);
        else
            playerAudio.PlayOneShot(stateSound, soundVolume);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (loopSound && playerAudio != null)
            playerAudio.StopLoop(this);
    }
}
