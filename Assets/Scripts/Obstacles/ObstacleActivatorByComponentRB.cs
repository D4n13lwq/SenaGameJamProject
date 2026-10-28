using UnityEngine;

public class ObstacleActivatorByComponentRB : MonoBehaviour
{
    [SerializeField] Rigidbody obstacle;
    [SerializeField] bool disableObstacleOnEnable;

    [SerializeField] float gravityMultiplier = 1f;

    private void OnEnable()
    {
        if (disableObstacleOnEnable)
        {
            obstacle.useGravity = false;
            obstacle.isKinematic = true;
        }
    }

    public void ActivateObstacle()
    {
        obstacle.isKinematic = false;
        obstacle.useGravity = true;
    }

    private void FixedUpdate()
    {
        if (obstacle == null || obstacle.isKinematic || !obstacle.useGravity) return;

        obstacle.AddForce(Physics.gravity * (gravityMultiplier - 1f), ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            ActivateObstacle();
    }
}