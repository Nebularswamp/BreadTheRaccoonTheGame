using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance = null;

    public AudioMixerGroup mixer;

    public Audio[] sounds;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        foreach(Audio sfx in sounds)
        {
            sfx.source = gameObject.AddComponent<AudioSource>();
            sfx.source.clip = sfx.clip;
            sfx.source.loop = sfx.loop;

            sfx.source.outputAudioMixerGroup = mixer;
        }

    }

    public void Play (string sound)
    {
        Audio sfx = Array.Find(sounds, item => item.name == sound); 
    }
    
    

}
