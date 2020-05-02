using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public struct Volume
    {
        public float music;
        public float sfx;
    }


    [System.Serializable]
    public struct Level
    {
        public string name;
        public AudioClip music;
        public AudioClip sfx;
        public Volume vol;
    };

    public Level[] Levels;

    public static AudioSource Music;
    public static AudioSource SFX;

    public static SoundManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        Music = transform.GetChild(0).GetComponent<AudioSource>();
        SFX = transform.GetChild(1).GetComponent<AudioSource>();

        if (Music.gameObject.name == "SFX" || SFX.gameObject.name == "Music")
        {
            AudioSource aux = Music;
            Music = SFX;
            SFX = aux;
        }

        UpdateSound();
    }


    public static void UpdateMusic(AudioClip NewMusic = null, float vol = 1f)
    {
        Music.clip = NewMusic;
        Music.volume = vol;
        Music.Play();
    }


    public static void UpdateSFX(AudioClip NewSFX = null, float vol = 1f)
    {
        SFX.clip = NewSFX;
        SFX.volume = vol;
        SFX.Play();
    }

    public static void UpdateSound()
    {

        for (int i = 0; i < instance.Levels.Length; i++)
        {
            if (SceneManager.GetActiveScene().name == instance.Levels[i].name)
            {
                UpdateMusic(instance.Levels[i].music, instance.Levels[i].vol.music);
                //Music.clip = instance.Levels[i].music;
                //Music.volume = instance.Levels[i].vol.music;
                //Music.Play();

                UpdateSFX(instance.Levels[i].sfx, instance.Levels[i].vol.sfx);
                //SFX.clip = instance.Levels[i].sfx;
                //SFX.volume = instance.Levels[i].vol.sfx;
                //SFX.Play();

                break;
            }
        }
    }

    public static void StopMusic()
    {
        Music.Pause();
        SFX.Pause();
    }
}

