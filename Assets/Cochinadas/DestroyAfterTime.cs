using System.Collections;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] float time;

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
