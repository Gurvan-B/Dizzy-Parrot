using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager: MonoBehaviour
{
    public Sound[] sounds;
    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;
    public static AudioManager instance;

    private void Awake()
    {
        instance = this;

        sfxAudioSource.volume = 0.5f;
        playMusic();
    }

    public void playSFX(string name, float volume)
    {
        Sound sound = getSoundByName(name);
        sfxAudioSource.PlayOneShot(sound.clip, volume);
    }

    private Sound getSoundByName(string name)
    {
        foreach (var sound in sounds)
        {
            if (sound.name == name)
            {
                return sound;
            }
        }
        return null;
    }

    public void playMusic()
    {
        musicAudioSource.Play();
    }

    public void stopMusic()
    {
        musicAudioSource.Stop();
    }
}
