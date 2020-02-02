using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    AudioSource musicAudioSource;

    [SerializeField]
    AnimationCurve fadeCurve;

    [SerializeField]
    float musicVolume = 1f;

    [SerializeField]
    float sfxVolume = 1f;
    
    public AudioClip mainMusic;
    public AudioClip endMusic;

    private void Start()
    {
        musicAudioSource.clip = mainMusic;
        musicAudioSource.Play();
    }

    public void FadeMusic(AudioClip clip, float fadeDuration)
    {
        StartCoroutine(MusicFadeCoroutine(clip, fadeDuration));
    }

    IEnumerator MusicFadeCoroutine(AudioClip clip, float fadeDuration)
    {
        float timer = 0.0f;
        bool changed = false;
        while(timer < fadeDuration)
        {
            float ratio = timer / fadeDuration;
            musicAudioSource.volume = fadeCurve.Evaluate(ratio) * musicVolume;
            if(ratio >= .5f && !changed)
            {
                musicAudioSource.clip = clip;
                musicAudioSource.Play();
                changed = true;
            }
            yield return null;
        }
        musicAudioSource.volume = musicVolume;
    }

    public void PlaySFX(AudioClip clip)
    {
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.volume = sfxVolume;
        source.clip = clip;
        source.Play();
        Destroy(source, clip.length + 0.1f);
    }
}
