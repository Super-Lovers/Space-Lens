using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    public AudioType AudioType;
    private List<SoundFile> _soundEffects = new List<SoundFile>();

    #region Components
    AudioManager _audioManager;
    AudioSource _audioSource;
    #endregion

    private void Start()
    {
        _audioManager = AudioManager.Instance;
        _audioSource = GetComponent<AudioSource>();
        List<AudioSource> soundSources = _audioManager.SoundSources;
        List<AudioSource> backgroundSources = _audioManager.BackgroundSources;

        if (AudioType == AudioType.Sound && soundSources.Contains(_audioSource) == false)
        {
            soundSources.Add(_audioSource);
            _audioSource.volume = _audioManager.SoundEffectsVolume;
        }
        else if (AudioType == AudioType.Background && backgroundSources.Contains(_audioSource) == false)
        {
            backgroundSources.Add(_audioSource);
            _audioSource.volume = _audioManager.BackgroundMusicVolume;
        }
    }

    public void PlaySound(string soundName)
    {
        bool isSoundFileFound = false;
        foreach (SoundFile soundFile in _soundEffects)
        {
            if (soundFile.Name == soundName)
            {
                isSoundFileFound = true;
                _audioSource.PlayOneShot(soundFile.AudioClip);
                break;
            }
        }

        if (isSoundFileFound == false)
        {
            Debug.Log($"The sound file <color=#ff0000>{soundName}</color> is not valid!");
        }
    }
}
