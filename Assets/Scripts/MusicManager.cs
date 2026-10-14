using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public static MusicManager instance;

    [SerializeField] private AudioSource MusicAudioSource;
    [SerializeField] private GameObject SFXPrefab;

    [SerializeField] private AudioClip MainMenuMusicClip;
    [SerializeField] private AudioClip ResultsMusicClip;
    [SerializeField] private AudioClip InstantButtonClip;
    [SerializeField] private List<AudioClip> LongButtonClips;
    [SerializeField] private List<AudioClip> EmojisClips;
    private void Awake()
    {
        instance = this;
    }

    public void StartMainMenuMusic()
    {
        if (MusicAudioSource.clip !=MainMenuMusicClip)
        {
            MusicAudioSource.clip = MainMenuMusicClip;
        }
        if (!MusicAudioSource.isPlaying)
        {
            MusicAudioSource.Play();
        }
    }

    public void StartResultsMusic()
    {
        if (MusicAudioSource.clip !=ResultsMusicClip)
        {
            MusicAudioSource.clip = ResultsMusicClip;
        }
        if (!MusicAudioSource.isPlaying)
        {
            MusicAudioSource.Play();
        }
    }

    public void StopMusic()
    {
        MusicAudioSource.Stop();
    }

    public void InstantSoundEffect()
    {
        GameObject newSFXGo = Instantiate(SFXPrefab);
        newSFXGo.GetComponent<SFXScript>().Initialize(InstantButtonClip);
        newSFXGo.transform.parent = transform;
    }

    public void LongButtonSoundEffect(int ID)
    {
        GameObject newSFXGo = Instantiate(SFXPrefab);
        newSFXGo.GetComponent<SFXScript>().Initialize(LongButtonClips[ID]);
        newSFXGo.transform.parent = transform;
    }

    public void EmojiKeyBoardSFX()
    {
        AudioClip audioClip = EmojisClips[Random.Range(0, EmojisClips.Count)];
        GameObject newSFXGo = Instantiate(SFXPrefab);
        newSFXGo.GetComponent<SFXScript>().Initialize(audioClip);
        newSFXGo.transform.parent = transform;
    }


}
