using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
public class LevelExit : MonoBehaviour
{
    [SerializeField] Transform doorPoint;

    public Transform DoorPoint => doorPoint != null ? doorPoint : transform;

    bool used;

    void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
        ObstacleReset.OnReset += ResetExit;
    }

    void OnDestroy()
    {
        ObstacleReset.OnReset -= ResetExit;
    }

    void ResetExit()
    {
        used = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (used) return;
        if (!other.TryGetComponent(out Player player) || !player.enabled) return;

        if (player.StartExit(this))
        {
            used = true;
            if (transform.GetComponentInChildren<Animator>() != null)
                transform.GetComponentInChildren<Animator>().SetTrigger("Open");
        }
    }

    public void Complete(Player player)
    {
        if (GameManager.Instance != null)
            GameManager.Instance.CompleteLevel();
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, DoorPoint.position);
        Gizmos.DrawWireSphere(DoorPoint.position, 0.15f);
    }
}
