using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HangZone : MonoBehaviour
{
    [SerializeField] Transform hangPoint;
    [SerializeField] float edgeMargin = 0.2f;

    Collider col;

    void Awake()
    {
        col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    public float MinX => col.bounds.min.x + edgeMargin;
    public float MaxX => col.bounds.max.x - edgeMargin;

    public Vector3 HangPositionAt(float x)
    {
        return new Vector3(Mathf.Clamp(x, MinX, MaxX), hangPoint.position.y, hangPoint.position.z);
    }

    void OnDrawGizmos()
    {
        Collider c = col != null ? col : GetComponent<Collider>();
        if (c == null || hangPoint == null) return;

        Gizmos.color = Color.cyan;
        Vector3 a = new(c.bounds.min.x + edgeMargin, hangPoint.position.y, hangPoint.position.z);
        Vector3 b = new(c.bounds.max.x - edgeMargin, hangPoint.position.y, hangPoint.position.z);
        Gizmos.DrawLine(a, b);
        Gizmos.DrawWireSphere(a, 0.1f);
        Gizmos.DrawWireSphere(b, 0.1f);
    }
}