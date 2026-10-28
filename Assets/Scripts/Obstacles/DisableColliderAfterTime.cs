using System.Collections;
using UnityEngine;

public class DisableColliderAfterTime : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] Collider coll;

    IEnumerator Deactive()
    {
        yield return new WaitForSeconds(time);
        coll.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Deactive());
    }
}
