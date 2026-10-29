using UnityEngine;

public class ObstacleActivatorByComponentRB : MonoBehaviour
{
    [SerializeField] Rigidbody obstacle;
    [SerializeField] bool disableObstacleOnEnable;

    [SerializeField] float gravityMultiplier = 1f;

    Vector3 startPosition;
    Quaternion startRotation;
    bool startKinematic;
    bool startUseGravity;

    private void Awake()
    {
        startPosition = obstacle.transform.position;
        startRotation = obstacle.transform.rotation;
        startKinematic = obstacle.isKinematic;
        startUseGravity = obstacle.useGravity;

        ObstacleReset.OnReset += ResetObstacle;
    }

    private void OnDestroy()
    {
        ObstacleReset.OnReset -= ResetObstacle;
    }

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

    void ResetObstacle()
    {
        if (obstacle == null) return;

        if (!obstacle.isKinematic)
        {
            obstacle.linearVelocity = Vector3.zero;
            obstacle.angularVelocity = Vector3.zero;
        }

        obstacle.isKinematic = disableObstacleOnEnable || startKinematic;
        obstacle.useGravity = !disableObstacleOnEnable && startUseGravity;

        obstacle.transform.SetPositionAndRotation(startPosition, startRotation);
        obstacle.position = startPosition;
        obstacle.rotation = startRotation;
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
