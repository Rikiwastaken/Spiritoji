using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public static MusicManager instance;

    [SerializeField] private AudioSource MainMenuMusicAudioSource;
    [SerializeField] private GameObject SFXPrefab;

    [SerializeField] private AudioClip MainMenuMusicClip;
    [SerializeField] private AudioClip InstantButtonClip;

    [SerializeField] private List<AudioClip> LongButtonClips;
    [SerializeField] private List<AudioClip> EmojisClips;
    private void Awake()
    {
        instance = this;
    }

    public void StartMainMenuMusic()
    {
        if (MainMenuMusicAudioSource.clip == null)
        {
            MainMenuMusicAudioSource.clip = MainMenuMusicClip;
        }
        if (!MainMenuMusicAudioSource.isPlaying)
        {
            MainMenuMusicAudioSource.Play();
        }
    }

    public void StopMainMenuMusic()
    {
        MainMenuMusicAudioSource.Stop();
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
