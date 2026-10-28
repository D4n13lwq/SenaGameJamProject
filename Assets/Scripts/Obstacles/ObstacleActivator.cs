using UnityEngine;

public class ObstacleActivator : MonoBehaviour
{
    [SerializeField] GameObject obstacle;
    [SerializeField] bool disableObstacleOnEnable;

    private void OnEnable()
    {
        if (disableObstacleOnEnable == true)
            obstacle.SetActive(false);
    }

    public void ActivateObstacle() { obstacle.SetActive(true); }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            ActivateObstacle();
    }
}
