using UnityEngine;

public class Audio
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    bool loop;

    [HideInInspector]
    public AudioSource source;
}
