using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * A simple audio manager that handles all tracks, how to play them and their basic characteristics
 */ 
public class MenuAudioManager : MonoBehaviour
{
    //List of all scene's/game's sounds
    public Sound[] sounds;

    void Awake()
    {

        //If we turn it into a manager, uncomment
        //DontDestroyOnLoad(gameObject);

        //Get all sounds in the list, set their source, clip, volume, pitch, loop... it's pretty self-explaining
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    //Handles playing an audio clip. Find them by name, report if nothing is found and play it.
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound: " + name + "not found!");
            return;
        }
        s.source.Play();
    }

    //When the menu appears, plays the background music
    void Start()
    {
        Play("MenuBGSnd");
    }
}
