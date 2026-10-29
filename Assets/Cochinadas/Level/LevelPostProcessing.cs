using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Volume))]
public class LevelPostProcessing : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] float chromaticMin = 0f;
    [SerializeField, Range(0f, 1f)] float chromaticMax = 1f;

    [SerializeField, Range(0f, 1f)] float vignetteMin = 0.2f;
    [SerializeField, Range(0f, 1f)] float vignetteMax = 0.6f;

    [SerializeField, Min(0f)] float transitionSpeed = 1f;

    ChromaticAberration chromaticAberration;
    Vignette vignette;

    void Awake()
    {
        VolumeProfile profile = GetComponent<Volume>().profile;

        if (!profile.TryGet(out chromaticAberration))
            chromaticAberration = profile.Add<ChromaticAberration>(true);
        if (!profile.TryGet(out vignette))
            vignette = profile.Add<Vignette>(true);

        chromaticAberration.active = true;
        vignette.active = true;
        chromaticAberration.intensity.overrideState = true;
        vignette.intensity.overrideState = true;
    }

    void Start()
    {
        float t = GetLevelProgress();
        chromaticAberration.intensity.value = Mathf.Lerp(chromaticMin, chromaticMax, t);
        vignette.intensity.value = Mathf.Lerp(vignetteMin, vignetteMax, t);
    }

    void Update()
    {
        float t = GetLevelProgress();
        float chromaticTarget = Mathf.Lerp(chromaticMin, chromaticMax, t);
        float vignetteTarget = Mathf.Lerp(vignetteMin, vignetteMax, t);

        if (transitionSpeed <= 0f)
        {
            chromaticAberration.intensity.value = chromaticTarget;
            vignette.intensity.value = vignetteTarget;
            return;
        }

        float step = transitionSpeed * Time.deltaTime;
        chromaticAberration.intensity.value = Mathf.MoveTowards(chromaticAberration.intensity.value, chromaticTarget, step);
        vignette.intensity.value = Mathf.MoveTowards(vignette.intensity.value, vignetteTarget, step);
    }

    float GetLevelProgress()
    {
        GameManager gm = GameManager.Instance;
        if (gm == null || gm.MaxLevel <= 1) return 0f;

        return Mathf.Clamp01((gm.CurrentLevel - 1f) / (gm.MaxLevel - 1f));
    }
}
