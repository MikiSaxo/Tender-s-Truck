using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public Sounds[] Sounds;
    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;
        foreach (Sounds s in Sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.loop = s.Loop;
        }
    }

    private void Start()
    {
        //Scene currentScene = SceneManager.GetActiveScene();
        //if (currentScene.name == "MainMenu")
        //else
        //    PlaySound("MusicGame");
        // PlaySound("MusicMain");
        StartCoroutine(WaitToLaunchMenuMusic());
    }

    IEnumerator WaitToLaunchMenuMusic()
    {
        yield return new WaitForSeconds(1f);
        PlaySound("MenuMusic");
    }

    public void PlaySound(string name)
    {
        Sounds s = Array.Find(Sounds, sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound : " + name + " not found");
            return;
        }
        s.Source.Play();
    }

    public void StopSound(string name)
    {
        Sounds s = Array.Find(Sounds, sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound : " + name + " not found in StopSound");
            return;
        }
        s.Source.Stop();
    }

    public void PlaySword()
    {
        int rdm = Random.Range(1, 5);
        PlaySound($"Sword{rdm}");
    }

    public float GetLengthMusic(string name)
    {
        Sounds s = Array.Find(Sounds, sound => sound.Name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound : " + name + " not found in StopSound");
            return 0;
        }

        return s.Clip.length;
    }
}