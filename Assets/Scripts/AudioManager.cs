using Unity.Audio;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class AudioManager : MonoBehaviour
{
    //atributes
    public Sound[] sounds;
    public Sound[] soundtracks;

    [HideInInspector]
    public Sound playingSoundTrack;

    public static AudioManager instance;

    //initialization
    void Awake()  //Singleton
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); 
        populateSoundList(sounds);
        populateSoundList(soundtracks);
        playingSoundTrack = null;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("Menu"))
        {
            PlaySoundTrack("Techno drum loop - Djfroyd");
        }
        else 
        {
            PlaySoundTrack();
        }   
    }

    //metodos principais
    public void Play(string name) 
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null) 
        {
            Debug.Log("Sound " + name + "Not found.");
            return;
        }

        s.source.Play();
    }


    public void PlaySoundTrack(string name)
    {
        Sound s = Array.Find(soundtracks, sound => sound.name == name);

        if (s == null)
        {
            Debug.Log("Sound " + name + "Not found.");
            return;
        }

        if (playingSoundTrack != null) 
        {
            StopPlaying(playingSoundTrack.name, soundtracks);
        }



        s.source.Play();
        playingSoundTrack = s;
    }


    public void PlaySoundTrack()
    {
        if (soundtracks.Length == 0) return;


        int index = UnityEngine.Random.Range(0, soundtracks.Length);

        Sound s = soundtracks[index];

        if (s == null)
        {
            Debug.Log("No soundtracks available.");
            return;
        }

        if (playingSoundTrack != null)
        {
            StopPlaying(playingSoundTrack.name, soundtracks);
        }

        s.source.Play();
        playingSoundTrack = s;
    }

    public void StopPlaying(string sound)
    {

        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + sound + " not found!");
            return;
        }


        s.source.Stop();
    }

    public void StopPlaying(string sound, Sound[] soundList)
    {
        Sound s = Array.Find(soundList, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + sound + " not found!");
            return;
        }

        s.source.Stop();
    }

    public List<Sound> PauseCurrentSounds()
    {
        List<Sound> PausedSounds = new List<Sound>();
        foreach (Sound s in sounds)
        {
            if(s.source.isPlaying)
            {
                
                PausedSounds.Add(s);
                s.source.Pause();
            }
        }

        return PausedSounds;
    }



    private void populateSoundList(Sound[] soundList) 
    {
        foreach (Sound s in soundList)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
}
