using UnityEngine;

public class PlayerDeadState : BasePlayerState
{
    [SerializeField] float disappearTime;
    [SerializeField] float rotationY;
    [SerializeField] float offsetZ;
    PlayerSpawner playerSpawner;
    float timer;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        player.Rb.isKinematic = true;
        player.Rb.useGravity = false;
        player.GetComponent<Collider>().enabled = false;
        player.enabled = false;
        timer = 0;
        player.transform.eulerAngles = new(player.transform.eulerAngles.x, rotationY, player.transform.eulerAngles.z);
        player.transform.position = new(player.transform.position.x, player.transform.position.y, player.transform.position.z + offsetZ);
        playerSpawner = FindAnyObjectByType<PlayerSpawner>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer >= disappearTime)
        {
            playerSpawner.SpawnPlayer();
            Destroy(player.gameObject);
        }
        else
            timer += Time.deltaTime;
    }
}
