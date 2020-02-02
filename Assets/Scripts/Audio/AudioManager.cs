using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private float increaseTime = 0;

    public Sound[] sounds;

    void Awake()
    {
        if (Instance != null)
            return;
        Instance = this;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource> ();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }
    }

    private Sound FindClip(string name)
    {
        Sound s = Array.Find (sounds, sound => sound.name == name);

        if (s != null)
            return s;

        else if (s == null)
            Debug.Log ($"{name}" + "<color=red> not found!</color>");

        return null;
    }

    private Sound FindClip(AudioClip clip)
    {
        Sound s = Array.Find (sounds, sound => sound.clip == clip);

        if (s != null)
            return s;

        else if (s == null)
            Debug.Log ($"{name}" + "<color=red> not found!</color>");

        return null;
    }

    public void Play(string name)
    {
        Sound s = FindClip (name);

        s.source.Play ();
    }

    public void Play(AudioClip clip)
    {
        Sound s = FindClip (clip);

        s.source.Play ();
    }

    public void Stop(string name)
    {
        Sound s = FindClip (name);

        s.source.Stop ();
    }

    public void Stop(AudioClip clip)
    {
        Sound s = FindClip (clip);

        s.source.Stop ();
    }

    public IEnumerator IncreaseVolume(string name)
    {
        Sound s = FindClip (name);
        float volume = s.volume;

        float time = 0;

        while (time < increaseTime)
        {
            s.source.volume = Mathf.Lerp (0, volume, time / increaseTime);

            time += Time.deltaTime;

            yield return null;
        }
    }

    public IEnumerator IncreaseVolume(AudioClip clip)
    {
        Sound s = FindClip (clip);
        float volume = s.volume;

        float time = 0;

        while (time < increaseTime)
        {
            s.source.volume = Mathf.Lerp (0, volume, time / increaseTime);

            time += Time.deltaTime;

            yield return null;
        }
    }
}
