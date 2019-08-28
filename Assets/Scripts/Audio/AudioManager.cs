using System;
using System.Collections.Generic;
using UnityEngine;

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
            if (value > 100) { _soundEffectsVolume = 100; }
            else if (value < 0) { _soundEffectsVolume = 0; }
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
            if (value > 100) { _backgroundMusicVolume = 100; }
            else if (value < 0) { _backgroundMusicVolume = 0; }
            else { _backgroundMusicVolume = value; }
            UpdateVolume();
        }
    }
    #endregion

    #region Components
    [NonSerialized]
    public AudioController AudioController;
    #endregion

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

        // TODO: Play sound effect to indicate the change was made.
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
}
