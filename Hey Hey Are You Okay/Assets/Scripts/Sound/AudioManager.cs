using System;
using UnityEngine;

namespace Sound
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField] Audio[] audios;

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
                a.source.loop = a.loop;
            }
        }

        void Start()
        {
            Play("BGM");

            if (!PlayerPrefs.HasKey("BGMVolume"))
            {
                PlayerPrefs.SetFloat("BGMVolume", GetAudio("BGM").volume);
            }
            SetVolume("BGM", PlayerPrefs.GetFloat("BGMVolume"));
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

        public void Stop(string name)
        {
            Audio a = Array.Find(audios, audio => audio.name == name);
            if (a == null)
            {
                Debug.LogWarning("Audio: " + name + " not found!");
                return;
            }

            a.source.Stop();
        }

        public void SetVolume(string name, float volume)
        {
            Audio a = Array.Find(audios, audio => audio.name == name);
            if (a == null)
            {
                Debug.LogWarning("Audio: " + name + " not found!");
                return;
            }

            a.source.volume = volume;
        }

        Audio GetAudio(string name)
        {
            Audio a = Array.Find(audios, audio => audio.name == name);
            if (a == null)
            {
                Debug.LogWarning("Audio: " + name + " not found!");
                return null;
            }
            return a;
        }

        public void ToggleMusic(bool toggle)
        {
            if (toggle)
                Play("BGM");
            else
                Stop("BGM");
        }

        public bool IsPlaying(string name)
        {
            Audio a = Array.Find(audios, audio => audio.name == name);
            if (a == null)
            {
                Debug.LogWarning("Audio: " + name + " not found!");
                return false;
            }

            if (a.source.isPlaying) return true;
            return false;
        }
    }
}