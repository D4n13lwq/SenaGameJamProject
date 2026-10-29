using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoxDamager : MonoBehaviour
{
    [SerializeField] float fallingThreshold = -0.1f;

    Rigidbody rb;

    float lastVelocityY;

    public bool IsDamaging => lastVelocityY < fallingThreshold || rb.linearVelocity.y < fallingThreshold;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        lastVelocityY = rb.isKinematic ? 0f : rb.linearVelocity.y;
    }

    private void OnCollisionEnter(Collision collision)
    {
        TryDamage(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        TryDamage(other.gameObject);
    }

    void TryDamage(GameObject other)
    {
        if (!enabled || rb.isKinematic || !IsDamaging) return;

        if (other.TryGetComponent(out Player player))
            player.Dead();
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying || rb == null) return;

        Gizmos.color = IsDamaging ? Color.red : Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 0.3f);
    }
}
