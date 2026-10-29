using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] AudioSource loopSource;
    [SerializeField] AudioSource oneShotSource;

    object loopOwner;

    void Awake()
    {
        if (loopSource == null) loopSource = CreateSource();
        if (oneShotSource == null) oneShotSource = CreateSource();

        loopSource.loop = true;
        oneShotSource.loop = false;
    }

    AudioSource CreateSource()
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.spatialBlend = 0f;
        return source;
    }

    public void PlayOneShot(AudioClip clip, float volume)
    {
        if (clip == null) return;
        oneShotSource.PlayOneShot(clip, volume);
    }

    public void PlayLoop(AudioClip clip, float volume, object owner)
    {
        if (clip == null) return;

        loopOwner = owner;
        loopSource.volume = volume;

        if (loopSource.isPlaying && loopSource.clip == clip) return;

        loopSource.clip = clip;
        loopSource.Play();
    }

    public void StopLoop(object owner)
    {
        if (loopOwner != owner) return;

        loopOwner = null;
        loopSource.Stop();
    }
}
