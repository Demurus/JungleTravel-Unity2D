using UnityEngine.Audio;
using UnityEngine;


[System.Serializable]
public class SoundClass 
{
    public string name;
    public AudioClip clip;
    [Range(0F,1F)]
    public float volume;

    [HideInInspector]
    public AudioSource source;

    public bool loop;
}
