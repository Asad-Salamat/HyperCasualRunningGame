using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject bgSound;
    public SoundData[] sounds;
    public static SoundManager Instance;
    private bool IsMusicOff, IsSoundOff;

    void Awake()
    {
        Instance = this;
    }

    void OnValidate()
    {
        if (sounds.Length == 0) return;
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].soundName = sounds[i].soundType.ToString();
        }
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = sounds[i].audioClip;
            audioSource.loop = false;
            audioSource.playOnAwake = false;
            sounds[i].audioSource = audioSource;
        }
        IsMusicOff = PlayerPrefs.GetInt("music", 0) != 0;
        IsSoundOff = PlayerPrefs.GetInt("sound", 0) != 0;
        print("Music " + IsMusicOff + " Sound " + IsSoundOff);
        UpdateMusicVolume(IsMusicOff);
        UpdateSoundVolume(IsSoundOff);
    }

    public void PlaySound(SoundType soundType)
    {
        if (IsSoundOff) return;
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].soundType == soundType)
            {
                sounds[i].audioSource.Play();
            }
        }
    }

    public void StopSound(SoundType soundType)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].soundType == soundType)
            {
                sounds[i].audioSource.Stop();
            }
        }
    }

    public void PlayBGMusic()
    {
        bgSound.SetActive(!IsMusicOff);
    }

    public void StopBGSound()
    {
        bgSound.GetComponent<AudioSource>().volume = .5f;
    }

    public void UpdateMusicVolume(bool _isMusicOff)
    {
        IsMusicOff = _isMusicOff;
        PlayBGMusic();
    }

    public void UpdateSoundVolume(bool _isSoundOff)
    {
        IsSoundOff = _isSoundOff;
    }

}

[System.Serializable]
public class SoundData
{
    public string soundName;
    public SoundType soundType;
    public AudioClip audioClip;
    [HideInInspector] public AudioSource audioSource;

}

public enum SoundType
{
    None,
    ButtonClick,
    Victory,
    LevelUp,
    WrongDoor,
    CorrectDoor,
    Lose,
    Collision
}
