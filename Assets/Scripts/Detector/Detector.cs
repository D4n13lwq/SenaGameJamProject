using UnityEngine;

public class Detector : MonoBehaviour
{
    [SerializeField] LayerMask allowedLayers;
    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 size;

    [SerializeField] Collider[] colliders;

    bool somethingDetected;

    private void FixedUpdate()
    {
        colliders = Physics.OverlapBox(transform.position + offset, size, transform.rotation, allowedLayers);
        somethingDetected = colliders.Length > 0;
    }

    public bool GetSomethingDetected() => somethingDetected;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + offset, size);
    }
}
