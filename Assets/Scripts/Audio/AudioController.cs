using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    [SerializeField]
    public AudioType AudioType;
    [SerializeField]
    private List<SoundFile> _soundEffects = new List<SoundFile>();

    #region Components
    AudioManager _audioManager;
    [System.NonSerialized]
    public AudioSource AudioSource;
    #endregion

    private void Start()
    {

        AudioSource = GetComponent<AudioSource>();
        _audioManager = AudioManager.Instance;
        if (_audioManager != null)
        {
            List<AudioSource> soundSources = _audioManager.SoundSources;
            List<AudioSource> backgroundSources = _audioManager.BackgroundSources;

            if (AudioType == AudioType.Sound && soundSources.Contains(AudioSource) == false)
            {
                soundSources.Add(AudioSource);
                AudioSource.volume = _audioManager.SoundEffectsVolume;
            }
            else if (AudioType == AudioType.Background && backgroundSources.Contains(AudioSource) == false)
            {
                backgroundSources.Add(AudioSource);
                AudioSource.volume = _audioManager.BackgroundMusicVolume;
            }
        }
    }

    public int CountOfSounds()
    {
        return _soundEffects.Count;
    }

    public void PlaySound(int soundIndex)
    {
        AudioSource.PlayOneShot(_soundEffects[soundIndex].AudioClip);
    }

    public void PlaySound(string soundName)
    {
        bool isSoundFileFound = false;
        foreach (SoundFile soundFile in _soundEffects)
        {
            if (soundFile.Name == soundName)
            {
                isSoundFileFound = true;
                AudioSource.PlayOneShot(soundFile.AudioClip);
                break;
            }
        }

        if (isSoundFileFound == false)
        {
            Debug.Log($"The sound file <color=#ff0000>{soundName}</color> is not valid!");
        }
    }
}
