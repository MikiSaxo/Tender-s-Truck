using UnityEngine;
using UnityEngine.Audio;
using System;
[Serializable] public class Sounds
{
    public string Name;
    public AudioClip Clip;
    [Range(0f, 1f)] public float Volume;
    public bool Loop;

    [HideInInspector] public AudioSource Source;
}