using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Struct-like class with the basic characteristics of an audio clip, like the name, file, volume, pitch, loop and source.

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
