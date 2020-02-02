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

    [SerializeField]
    float transitionSfxVolume = 1f;
    [SerializeField]
    float appearVolume = 1f;

    public AudioClip mainMusicIntro;
    public AudioClip mainMusic;
    public AudioClip endMusic;

    public AudioClip[] appearSound;
    public AudioClip[] goToNextSound;

    public void PlayAppearSound()
    {
        PlaySFX(appearSound[Random.Range(0, appearSound.Length)], appearVolume);        
    }

    public void PlayGoToNextSound()
    {
        PlaySFX(goToNextSound[Random.Range(0, goToNextSound.Length)], transitionSfxVolume);
    }


    IEnumerator Start()
    {
        musicAudioSource.loop = false;
        musicAudioSource.clip = mainMusicIntro;
        musicAudioSource.Play();

        yield return new WaitWhile(() => musicAudioSource.isPlaying);

        musicAudioSource.loop = true;
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

    public void PlaySFX(AudioClip clip, float _volume)
    {
        if (clip == null)
            return;

        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.volume = _volume;
        source.clip = clip;
        source.Play();
        Destroy(source, clip.length + 0.1f);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null)
            return;

        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.volume = sfxVolume;
        source.clip = clip;
        source.Play();
        Destroy(source, clip.length + 0.1f);
    }

}
