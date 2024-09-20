using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Function;

public class AudioSourceIndexer
{
    private AudioSource _bgmSource;
    private AudioSource _sfxSource;

    public AudioSource this[SoundType type]
    {
        get
        {
            switch (type)
            {
                case SoundType.Bgm:
                    {
                        if(_bgmSource == null)
                        {
                            Debug.Log("Bgm audio source has not assigned");
                        }
                        return _bgmSource;
                    }
                    
                case SoundType.Sfx:
                    {
                        if (_sfxSource == null)
                        {
                            Debug.Log("sfx audio source has not assigned");
                        }
                        return _sfxSource;
                    }
                    
                default:
                    {
                        Debug.Log($"Undefined type : {type}");
                        return null;
                    }
            }
        }
        set
        {
            switch (type)
            {
                case SoundType.Bgm:
                    {
                        _bgmSource = value; 
                        break;
                    }
                case SoundType.Sfx:
                    {
                        _sfxSource = value;
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
