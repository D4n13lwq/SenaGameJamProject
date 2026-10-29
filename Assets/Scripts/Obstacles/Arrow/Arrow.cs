using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float destroyTimeOnCollision;

    [SerializeField] AudioClip impactSound;
    [SerializeField, Range(0f, 1f)] float impactVolume = 1f;
    [SerializeField, Range(0f, 1f)] float impactSpatialBlend = 0f;
    [SerializeField, Range(0f, 0.5f)] float impactPitchVariation = 0.1f;

    Rigidbody rb;
    bool hasImpacted;

    private void Awake()
    {
        ObstacleReset.OnReset += ResetObstacle;
    }

    private void OnDestroy()
    {
        ObstacleReset.OnReset -= ResetObstacle;
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward.normalized * speed;
    }

    void ResetObstacle()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hasImpacted) return;
        hasImpacted = true;

        Vector3 hitPoint = collision.contactCount > 0 ? collision.GetContact(0).point : transform.position;
        SoundUtility.PlayAt(impactSound, hitPoint, impactVolume, impactSpatialBlend, impactPitchVariation);

        rb.linearVelocity = Vector3.zero;
        rb.useGravity = false;
        rb.isKinematic = true;
        StartCoroutine(DestroyArrow());
    }

    IEnumerator DestroyArrow()
    {
        yield return new WaitForSeconds(destroyTimeOnCollision);
        Destroy(gameObject);
    }
}
