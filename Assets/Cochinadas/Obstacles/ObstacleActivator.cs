using UnityEngine;

public class ObstacleActivator : MonoBehaviour
{
    [SerializeField] GameObject obstacle;
    [SerializeField] bool disableObstacleOnEnable;

    bool obstacleStartsActive;

    Vector3 startPosition;
    Quaternion startRotation;
    Rigidbody obstacleRb;

    private void Awake()
    {
        obstacleStartsActive = !disableObstacleOnEnable && obstacle.activeSelf;

        startPosition = obstacle.transform.position;
        startRotation = obstacle.transform.rotation;
        obstacleRb = obstacle.GetComponent<Rigidbody>();

        ObstacleReset.OnReset += ResetObstacle;
    }

    private void OnDestroy()
    {
        ObstacleReset.OnReset -= ResetObstacle;
    }

    private void OnEnable()
    {
        if (disableObstacleOnEnable == true)
            obstacle.SetActive(false);
    }

    public void ActivateObstacle() { obstacle.SetActive(true); }

    void ResetObstacle()
    {
        if (obstacle == null) return;

        obstacle.SetActive(false);

        obstacle.transform.SetPositionAndRotation(startPosition, startRotation);

        if (obstacleRb != null)
        {
            if (!obstacleRb.isKinematic)
            {
                obstacleRb.linearVelocity = Vector3.zero;
                obstacleRb.angularVelocity = Vector3.zero;
            }
            obstacleRb.position = startPosition;
            obstacleRb.rotation = startRotation;
        }

        if (obstacleStartsActive)
            obstacle.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            ActivateObstacle();
    }
}
