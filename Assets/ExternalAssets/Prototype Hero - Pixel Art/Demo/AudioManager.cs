using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string m_name;
    public bool background;
    public AudioClip[] m_clips;

    [Range(0f, 1f)]
    public float volume = 0f;
    
    [Range(0f, 1.5f)]
    public float pitch = 1.0f;

    public bool m_loop = false;

    private AudioSource m_source;

    public AudioMixerGroup audioMixer;

    public void SetSource(AudioSource source)
    {
        m_source = source;
        if (m_clips.Length > 0)
        {
            int randomClip = Random.Range(0, m_clips.Length - 1);
            m_source.clip = m_clips[randomClip];
        }
        else
            m_source.clip = m_clips[0];
        
        m_source.loop = m_loop;
        m_source.outputAudioMixerGroup = audioMixer;
    }

    public void Play()
    {
        if(m_clips.Length > 1)
        {
            int randomClip = Random.Range(0, m_clips.Length - 1);
            m_source.clip = m_clips[randomClip];
        }
        m_source.volume = volume;
        m_source.pitch = pitch;
        m_source.Play();
    }

    public void Stop()
    {
        m_source.Stop();
    }

    public bool IsPlaying()
    {
        if (m_source.isPlaying)
            return true;
        else
            return false;
    }
}

public class AudioManager : MonoBehaviour
{
    // Make it a singleton class that can be accessible everywhere
    public static AudioManager instance;

    [SerializeField]
    Sound[] m_sounds;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        for(int i = 0; i < m_sounds.Length; i++)
        {
            GameObject go = new GameObject("Sound_" + i + "_" + m_sounds[i].m_name);
            go.transform.SetParent(transform);
            m_sounds[i].SetSource(go.AddComponent<AudioSource>());
        }
    }

    public void PlaySound (string name)
    {
        for(int i = 0; i < m_sounds.Length; i++)
        {
            if(m_sounds[i].m_name == name)
            {
                m_sounds[i].Play();
                return;
            }
        }

        return;
    }

    public void StopSound(string name)
    {
        for (int i = 0; i < m_sounds.Length; i++)
        {
            if (m_sounds[i].m_name == name && m_sounds[i].IsPlaying())
            {
                m_sounds[i].Stop();
                return;
            }
        }
    }

    public void StopAllSounds()
    {
        for (int i = 0; i < m_sounds.Length; i++)
        {
            m_sounds[i].Stop();
        }
    }

    public void StopAllBackgroundSounds()
    {
        for (int i = 0; i < m_sounds.Length; i++)
        {
            if (m_sounds[i].background == true)
            {
                m_sounds[i].Stop();
            }
        }
    }

    //public void SetVolumeEffect(float volume)
    //{
    //    for (int i = 0; i < m_sounds.Length; i++)
    //    {
    //        if (m_sounds[i].background == false)
    //        {
    //            m_sounds[i].volume = volume;
    //        }
    //    }
    //}

    //public void SetVolumeBackground(float volume)
    //{
    //    for (int i = 0; i < m_sounds.Length; i++)
    //    {
    //        if (m_sounds[i].background == true)
    //        {
    //            m_sounds[i].volume = volume;
    //        }
    //    }
    //}

    public bool IsPlaying(string name)
    {
        for (int i = 0; i < m_sounds.Length; i++)
        {
            if (m_sounds[i].m_name == name && m_sounds[i].IsPlaying())
            {
                return true;
            }
        }

        return false;
    }

    public bool IsPlayingBackground()
    {
        for (int i = 0; i < m_sounds.Length; i++)
        {
            if (m_sounds[i].background == true && m_sounds[i].IsPlaying())
            {
                return true;
            }
        }

        return false;
    }

    public string ReturnCurrentBackgroundMusicPlaying()
    {
        for (int i = 0; i < m_sounds.Length; i++)
        {
            if (m_sounds[i].background == true && m_sounds[i].IsPlaying())
            {
                return m_sounds[i].m_name;
            }
        }

        return null;
    }
}
