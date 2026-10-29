using UnityEngine;

public class ArrowLauncher : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform arrowPivot;

    [SerializeField] AudioClip launchSound;
    [SerializeField, Range(0f, 1f)] float launchVolume = 1f;
    [SerializeField, Range(0f, 1f)] float launchSpatialBlend = 0f;
    [SerializeField, Range(0f, 0.5f)] float launchPitchVariation = 0.1f;

    private void OnEnable() 
    {
        if (arrowPivot == null)
            arrowPivot = transform;
    }

    public void LaunchArrow()
    {
        Instantiate(arrowPrefab, arrowPivot.position, arrowPivot.rotation);
        SoundUtility.PlayAt(launchSound, arrowPivot.position, launchVolume, launchSpatialBlend, launchPitchVariation);
    }
}
