using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Function;

public class SoundManager : MonoSingleton<SoundManager>
{
    private AudioSourceIndexer _audioSources = new AudioSourceIndexer();

    private void Awake()
    {
        AudioSource[] sourceArr = GetComponents<AudioSource>();

        _audioSources[SoundType.Bgm] = sourceArr[0];
        _audioSources[SoundType.Sfx ] = sourceArr[1];

        _audioSources[SoundType.Bgm].loop = true;
    }

    public void PlayBGM(AudioClip clip)
    {
        _audioSources[SoundType.Bgm].Stop();
        _audioSources[SoundType.Bgm].clip = clip;
        _audioSources[SoundType.Bgm].Play();
    }

    public void StopBgm()
    {
        _audioSources[SoundType.Bgm].Stop();
    }

    public float SetVolume(SoundType type)
    {
        if (_audioSources[type].volume < 0.5f)
        {
            _audioSources[type].volume += 0.25f;
        }
        else
        {
            _audioSources[type].volume = 0f;
        }

        return _audioSources[type].volume;
    }

    public float SetVolume(SoundType type, float value)
    {
        _audioSources[type].volume = value;
        return _audioSources[type].volume;
    }

    public float GetVolume(SoundType type)
    {
        return _audioSources[type].volume;
    }

    public void PlaySFX(AudioClip clip)
    {
        _audioSources[SoundType.Sfx].PlayOneShot(clip, _audioSources[SoundType.Sfx].volume);
    }
}
