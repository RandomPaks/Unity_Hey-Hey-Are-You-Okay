using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public Audio[] audios;

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        DontDestroyOnLoad(gameObject);

        foreach (Audio a in audios)
        {
            a.source = gameObject.AddComponent<AudioSource>();
            a.source.clip = a.clip;

            a.source.volume = a.volume;
            a.source.pitch = a.pitch;
        }
    }

    void Start()
    {
        //play bgm
    }

    public void Play(string name)
    {
        Audio a = Array.Find(audios, audio => audio.name == name);
        if (a == null)
        {
            Debug.LogWarning("Audio: " + name + " not found!");
            return;
        }

        a.source.Play();
    }
}
