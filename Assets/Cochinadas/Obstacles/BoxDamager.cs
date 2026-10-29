using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoxDamager : MonoBehaviour
{
    [SerializeField] float fallingThreshold = -0.1f;
    [SerializeField] GameObject brokenBox;
    [SerializeField] float YVelocity;

    Vector3 startPosition;
    Quaternion startRotation;

    Rigidbody rb;

    float lastVelocityY;

    public bool IsDamaging => lastVelocityY < fallingThreshold || rb.linearVelocity.y < fallingThreshold;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void OnEnable()
    {
        rb.linearVelocity = new(rb.linearVelocity.x, YVelocity, rb.linearVelocity.z);
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
        {
            player.Dead();
            Instantiate(brokenBox, transform.position, transform.rotation);
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying || rb == null) return;

        Gizmos.color = IsDamaging ? Color.red : Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 0.3f);
    }
}
