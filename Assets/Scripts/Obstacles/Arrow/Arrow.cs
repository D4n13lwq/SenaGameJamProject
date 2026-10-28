using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float destroyTimeOnCollision;
    Rigidbody rb;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward.normalized * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
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
