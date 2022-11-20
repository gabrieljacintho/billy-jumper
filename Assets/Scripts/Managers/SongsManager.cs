using System;
using UnityEngine;

public class SongsManager : MonoBehaviour
{
    public static SongsManager instance;
    
    public AudioSource audioSource;

    [SerializeField]
    private AudioClip[] songs;

    [Header("AudioClips")]
    [SerializeField]
    private AudioClip buttonClickAudioClip;
    [SerializeField]
    private AudioClip collectedCoinAudioClip;
    [SerializeField]
    private AudioClip finishedLevelAudioClip;
    [SerializeField]
    private AudioClip loadedSceneAudioClip;

    [Space]
    [SerializeField]
    private float soundEffectsVolumeScale = 0.5f;

    [NonSerialized]
    public bool isPlaying = true;


    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Update()
    {
        if (!audioSource.isPlaying && isPlaying)
        {
            audioSource.clip = songs[UnityEngine.Random.Range(0, songs.Length)];
            audioSource.Play();
        }
    }

    public bool Mute()
    {
        if (isPlaying) audioSource.Pause();
        else audioSource.Play();

        return isPlaying = isPlaying ? false : true;
    }

    public void ChangeSongButton()
    {
        AudioClip currentAudioClip = audioSource.clip;
        while (currentAudioClip == audioSource.clip)
        {
            audioSource.clip = songs[UnityEngine.Random.Range(0, songs.Length)];
        }
        audioSource.Play();
    }

    public void PlayButtonClickAudioClip()
    {
        if (isPlaying) audioSource.PlayOneShot(buttonClickAudioClip, soundEffectsVolumeScale);
    }

    public void PlayCollectedCoinAudioClip()
    {
        if (isPlaying) audioSource.PlayOneShot(collectedCoinAudioClip, soundEffectsVolumeScale);
    }

    public void PlayFinishedLevelAudioClip()
    {
        if (isPlaying) audioSource.PlayOneShot(finishedLevelAudioClip, soundEffectsVolumeScale);
    }

    public void PlayLoadedSceneAudioClip()
    {
        if (isPlaying) audioSource.PlayOneShot(loadedSceneAudioClip, soundEffectsVolumeScale);
    }
}
