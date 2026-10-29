using UnityEngine;

public static class SoundUtility
{
    public static void PlayAt(AudioClip clip, Vector3 position, float volume = 1f, float spatialBlend = 0f, float pitchVariation = 0f)
    {
        if (clip == null) return;

        GameObject go = new GameObject("TempSound_" + clip.name);
        go.transform.position = position;

        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.spatialBlend = spatialBlend;
        source.pitch = 1f + Random.Range(-pitchVariation, pitchVariation);
        source.Play();

        Object.Destroy(go, clip.length / Mathf.Max(0.01f, Mathf.Abs(source.pitch)) + 0.1f);
    }
}
