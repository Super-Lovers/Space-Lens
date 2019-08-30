using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    // Types of audio sources
    public List<AudioSource> BackgroundSources = new List<AudioSource>();
    public List<AudioSource> SoundSources = new List<AudioSource>();

    #region Audio volume controls
    private float _previousSoundEffectsVolume;
    [SerializeField]
    private float _soundEffectsVolume;
    public float SoundEffectsVolume
    {
        get { return _soundEffectsVolume; }
        set
        {
            // We validate whether or not the volume is greater
            // than or less than it should be, to avoid exceptions.
            if (value >= 100) { _soundEffectsVolume = 100; }
            else if (value <= 0) { _soundEffectsVolume = 0; }
            else { _soundEffectsVolume = value; }
            UpdateVolume();
        }
    }

    private float _previousBackgroundMusicVolume;
    [SerializeField]
    private float _backgroundMusicVolume;
    public float BackgroundMusicVolume
    {
        get { return _backgroundMusicVolume; }
        set
        {
            if (value >= 100) { _backgroundMusicVolume = 100; }
            else if (value <= 0) { _backgroundMusicVolume = 0; }
            else { _backgroundMusicVolume = value; }
            UpdateVolume();
        }
    }
    #endregion

    #region Components
    [NonSerialized]
    public AudioController AudioController;
    #endregion

    [Header("Images to display when updating volume level in order.")]
    [Space(10)]
    [SerializeField]
    private List<Sprite> _audioVolumeLevels = new List<Sprite>(4);
    [SerializeField]
    private Image _backgroundVolumeIcon = null;
    [SerializeField]
    private Image _soundVolumeIcon = null;

    private void Awake()
    {
        AudioController = GetComponent<AudioController>();

        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }
    
    /// <summary>
    /// This function runs whenever the audio settings' volume parameter of
    /// the audio sources in the scenes is updated by the player.
    /// </summary>
    public void UpdateVolume()
    {
        foreach (AudioSource audioSource in SoundSources)
        {
            if (audioSource != null)
            {
                audioSource.volume = SoundEffectsVolume / 100f;
            }
        }

        foreach (AudioSource audioSource in BackgroundSources)
        {
            if (audioSource != null)
            {
                audioSource.volume = BackgroundMusicVolume / 100f;
            }
        }
        
        UpdateIcons();
        // TODO: Play sound effect to indicate the change was made.
        AudioController.PlaySound("Volume Update");
    }

    #region Sound effects events
    public void MuteSoundEffects()
    {
        _previousSoundEffectsVolume = SoundEffectsVolume;
        SoundEffectsVolume = 0;
    }

    public void UnmuteSoundEffects()
    {
        SoundEffectsVolume = _previousSoundEffectsVolume;
    }

    public void IncreaseSoundVolume()
    {
        if (SoundEffectsVolume <= 90)
        {
            SoundEffectsVolume += 10;
        }
    }

    public void DecreaseSoundVolume()
    {
        if (SoundEffectsVolume >= 10)
        {
            SoundEffectsVolume -= 10;
        }
    }
    #endregion

    #region Background music events
    public void MuteBackgroundMusic()
    {
        _previousBackgroundMusicVolume = BackgroundMusicVolume;
        BackgroundMusicVolume = 0;
    }

    public void UnmuteBackgroundMusic()
    {
        BackgroundMusicVolume = _previousBackgroundMusicVolume;
    }

    public void IncreaseBackgroundVolume()
    {
        if (BackgroundMusicVolume <= 90)
        {
            BackgroundMusicVolume += 10;
        }
    }

    public void DecreaseBackgroundVolume()
    {
        if (BackgroundMusicVolume >= 10)
        {
            BackgroundMusicVolume -= 10;
        }
    }
    #endregion

    public void UpdateIcons()
    {
        if (BackgroundMusicVolume <= 9)
        {
            _backgroundVolumeIcon.sprite = _audioVolumeLevels[0];
        }
        else if (BackgroundMusicVolume > 9 && BackgroundMusicVolume < 25)
        {
            _backgroundVolumeIcon.sprite = _audioVolumeLevels[1];
        } else if (BackgroundMusicVolume >= 25 && BackgroundMusicVolume < 50)
        {
            _backgroundVolumeIcon.sprite = _audioVolumeLevels[2];
        }
        else if (BackgroundMusicVolume >= 50 && BackgroundMusicVolume < 75)
        {
            _backgroundVolumeIcon.sprite = _audioVolumeLevels[3];
        }
        else if (BackgroundMusicVolume >= 75 && BackgroundMusicVolume <= 100)
        {
            _backgroundVolumeIcon.sprite = _audioVolumeLevels[4];
        }

        if (SoundEffectsVolume <= 9)
        {
            _soundVolumeIcon.sprite = _audioVolumeLevels[0];
        } 
        else if (SoundEffectsVolume > 9 && SoundEffectsVolume < 25)
        {
            _soundVolumeIcon.sprite = _audioVolumeLevels[1];
        }
        else if (SoundEffectsVolume >= 25 && SoundEffectsVolume < 50)
        {
            _soundVolumeIcon.sprite = _audioVolumeLevels[2];
        }
        else if (SoundEffectsVolume >= 50 && SoundEffectsVolume < 75)
        {
            _soundVolumeIcon.sprite = _audioVolumeLevels[3];
        }
        else if (SoundEffectsVolume >= 75 && SoundEffectsVolume <= 100)
        {
            _soundVolumeIcon.sprite = _audioVolumeLevels[4];
        }
    }
}
