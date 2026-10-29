using System.Collections;
using UnityEngine;

public class DisableColliderAfterTime : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] Collider coll;

    bool startEnabled;

    private void Awake()
    {
        startEnabled = coll.enabled;
        ObstacleReset.OnReset += ResetObstacle;
    }

    private void OnDestroy()
    {
        ObstacleReset.OnReset -= ResetObstacle;
    }

    IEnumerator Deactive()
    {
        yield return new WaitForSeconds(time);
        coll.enabled = false;
    }

    void ResetObstacle()
    {
        StopAllCoroutines();
        if (coll != null)
            coll.enabled = startEnabled;
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Deactive());
    }
}
