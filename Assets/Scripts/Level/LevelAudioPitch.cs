using UnityEngine;

public class LevelAudioPitch : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [SerializeField, Range(-3f, 3f)] float minPitch = 1f;
    [SerializeField, Range(-3f, 3f)] float maxPitch = 1.5f;

    [SerializeField, Min(0f)] float transitionSpeed = 0.5f;

    void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (audioSource != null)
            audioSource.pitch = GetTargetPitch();
    }

    void Update()
    {
        if (audioSource == null) return;

        float target = GetTargetPitch();

        if (transitionSpeed <= 0f)
            audioSource.pitch = target;
        else
            audioSource.pitch = Mathf.MoveTowards(audioSource.pitch, target, transitionSpeed * Time.deltaTime);
    }

    float GetTargetPitch()
    {
        return Mathf.Lerp(minPitch, maxPitch, GetLevelProgress());
    }

    float GetLevelProgress()
    {
        GameManager gm = GameManager.Instance;
        if (gm == null || gm.MaxLevel <= 1) return 0f;

        return Mathf.Clamp01((gm.CurrentLevel - 1f) / (gm.MaxLevel - 1f));
    }
}
