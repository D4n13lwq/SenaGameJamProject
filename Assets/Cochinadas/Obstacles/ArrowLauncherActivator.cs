using UnityEngine;

public class ArrowLauncherActivator : MonoBehaviour
{
    [SerializeField] ArrowLauncher obstacle;
    [SerializeField] bool disableObstacleOnEnable;
    [SerializeField] float repetitionTime;
    float timer;

    private void Awake()
    {
        ObstacleReset.OnReset += ResetObstacle;
    }

    private void OnDestroy()
    {
        ObstacleReset.OnReset -= ResetObstacle;
    }

    public void ActivateObstacle()
    {
        timer = 0;
        obstacle.LaunchArrow();
    }

    void ResetObstacle()
    {
        timer = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
            ActivateObstacle();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            timer += Time.deltaTime;
            if (timer >= repetitionTime)
                ActivateObstacle();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null)
            timer = 0;
    }
}
