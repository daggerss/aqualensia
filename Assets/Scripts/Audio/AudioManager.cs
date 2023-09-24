using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    void Start()
    {
        // PlayMusic("Theme");
    }

    // For Music that loops
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, sound => sound.Name == name);

        if (s == null)
        {
            Debug.LogWarning("Music: " + name + " not found");
            return;
        }
        else
        {
            musicSource.clip = s.Clip;
            musicSource.volume = s.Volume;
            musicSource.pitch = s.Pitch;
            musicSource.Play();
        }
    }

    // For SFX that plays once only
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, sound => sound.Name == name);

        if (s == null)
        {
            Debug.LogWarning("SFX: " + name + " not found");
            return;
        }
        else
        {
            sfxSource.pitch = s.Pitch;
            sfxSource.PlayOneShot(s.Clip, s.Volume);
        }
    }

    // For existing audio that needs to play concurrently w the above
    public void PlaySeparate(string name)
    {
        // Try music
        Sound s = Array.Find(musicSounds, sound => sound.Name == name);

        if (s == null) // Not music
        {
            Debug.LogWarning(name + " not found in Music");

            // Try sfx
            s = Array.Find(sfxSounds, sound => sound.Name == name);

            if (s == null) // Not sfx
            {
                Debug.LogWarning(name + " not found in SFX");
                return;
            }
        }

        if (s != null) // Found music or sfx
        {
            AudioSource tempSource = gameObject.AddComponent<AudioSource>();
            
            tempSource.clip = s.Clip;
            tempSource.volume = s.Volume;
            tempSource.pitch = s.Pitch;
            tempSource.loop = s.Loop;
            tempSource.Play();

            Destroy(tempSource);
        }
    }
}
