using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] musicSounds, ambientSounds, sfxSounds;
    public AudioSource musicSource, ambientSource, sfxSource;

    void Start()
    {
        PlayMusic("Main Theme");
    }

    public void PlaySceneAudio(string sceneName)
    {
        if (sceneName == "Start")
        {
            PlayMusic("Main Theme");
        }

        else if (sceneName == "Overworld")
        {
            PlayMusic("Harbor");
        }

        else if (sceneName == "Main Hall" || sceneName == "Shop")
        {
            PlayMusic("MARLIN");
        }

        else if (sceneName == "C1" || sceneName == "C2" || sceneName == "C3" ||
                 sceneName == "S1" || sceneName == "S2" || sceneName == "S3" ||
                 sceneName == "O1" || sceneName == "O2" || sceneName == "O3")
        {
            // Ambient
            PlayAmbience("Underwater");

            // Music
            if (sceneName == "C1" || sceneName == "C2" || sceneName == "C3")
            {
                PlayMusic("Coral Reef");
            }

            // TODO: Seagrass & Open Ocean
        }
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

    // For ambience
    public void PlayAmbience(string name)
    {
        Sound s = Array.Find(ambientSounds, sound => sound.Name == name);

        if (s == null)
        {
            Debug.LogWarning("Ambience: " + name + " not found");
            return;
        }
        else
        {
            ambientSource.clip = s.Clip;
            ambientSource.volume = s.Volume;
            ambientSource.pitch = s.Pitch;
            ambientSource.Play();
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
        else if (!sfxSource.isPlaying)
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
