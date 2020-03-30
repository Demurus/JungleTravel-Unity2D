using UnityEngine.Audio;
using UnityEngine;
using System;


public class AudioManager : MonoBehaviour
{
    
    public SoundClass[] sounds;

    void Awake()
    {
        foreach (SoundClass s in sounds)
        {
            s.source=gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
            
    }
   
    public void Play(string name)
    {
        SoundClass s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Stop(string name)
    {
        SoundClass s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public void PlayOnce (AudioClip name)
    {
        SoundClass s = Array.Find(sounds, sound => sound.clip == name);
        s.source.PlayOneShot(name);
    }
}
