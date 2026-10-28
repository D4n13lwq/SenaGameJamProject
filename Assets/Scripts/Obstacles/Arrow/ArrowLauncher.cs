using UnityEngine;

public class ArrowLauncher : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform arrowPivot;

    private void OnEnable() 
    {
        if (arrowPivot == null)
            arrowPivot = transform;
    }

    public void LaunchArrow()
    {
        Instantiate(arrowPrefab, arrowPivot.position, arrowPivot.rotation);
    }
}
