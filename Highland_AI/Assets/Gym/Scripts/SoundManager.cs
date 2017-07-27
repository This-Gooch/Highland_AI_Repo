using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance;

    public ClipStruct[] _Sounds;
    public ClipStruct[] _Musics;

    private Dictionary<string, AudioClip> m_Sounds;
    private Dictionary<string, AudioClip> m_Musics; 

    private AudioSource aud_music;
    private AudioSource aud_sounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    //Create the audiosources
    private void Start()
    {

        aud_music = gameObject.AddComponent<AudioSource>();
        aud_sounds = gameObject.AddComponent<AudioSource>();
    }

    private void LoadDictionaries()
    {
        for (int i = 0; i < _Sounds.Length; i++)
        {
            m_Musics.Add(_Musics[i].name, _Musics[i].clip);
        }

        for (int i = 0; i < _Musics.Length; i++)
        {
            m_Sounds.Add(_Sounds[i].name, _Sounds[i].clip);
        }
    }

    //

}

[System.Serializable]
public struct ClipStruct
{
    public string name;
    public AudioClip clip;
}
