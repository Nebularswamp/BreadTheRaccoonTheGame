using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Audio
{
    public string name;
    public AudioClip clip;

    public bool loop = false;

    public AudioMixerGroup mixergroup;

    [HideInInspector]
    public AudioSource source;
}
