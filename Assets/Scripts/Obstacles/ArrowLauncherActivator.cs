using UnityEngine;

public class ArrowLauncherActivator : MonoBehaviour
{
    [SerializeField] ArrowLauncher obstacle;
    [SerializeField] bool disableObstacleOnEnable;
    [SerializeField] float repetitionTime;
    float timer;

    public void ActivateObstacle()
    {
        timer = 0;
        obstacle.LaunchArrow();
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
