using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static bool songInPause = true;
    public static bool soundFxInPause = true;
    private float _musicMult = 1, _fxMult = 1;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    instance = new GameObject("GlobalAudioManger", typeof(AudioManager)).GetComponent<AudioManager>();
                }
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    public static AudioSource musicSource;
    public static AudioSource sfxSource;
    public static AudioSource sfxPitchSource;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        musicSource = this.gameObject.AddComponent<AudioSource>();
        sfxSource = this.gameObject.AddComponent<AudioSource>();
        sfxPitchSource = this.gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;

        //AudioMixer mix = Resources.Load("Mixer") as AudioMixer;
        //musicSource.outputAudioMixerGroup = mix.FindMatchingGroups("music")[0];
        //sfxSource.outputAudioMixerGroup = mix.FindMatchingGroups("fx")[0];
    }
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogError("Se intenta reproducir una musica vacia");
            return;
        }

        musicSource.clip = clip;
        musicSource.Play();
    }
    public void PlayMusic()
    {
        if (musicSource.clip == null)
        {
            Debug.LogError("MusicSource no tiene clip");
            return;
        }
        musicSource.Play();
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void StopSFX()
    {
        sfxSource.Stop();
    }


    /// <summary>
    /// Escribir a partir de Music/
    /// </summary>
    /// <param name="path"></param>
    /// <param name="volume"></param>
    public void PlayMusic(string path, float volume)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/Music/" + path);
        if (clip == null)
        {
            Debug.LogError("Se intenta reproducir una musica vacio");
            return;
        }
        PlayMusic(clip, volume);
    }
    ///AGREGADOS POR OMAR ///////
    public void PlayMusic(AudioClip clip, float volume)
    {
        if (clip == null)
        {
            Debug.LogError("********Se intenta reproducir una musica vacia");
            return;
        }
        if (musicSource.clip == clip)
        {
            if (musicSource.isPlaying == false)
            {
                musicSource.Play();
                return;
            }
            return;
        }

        musicSource.clip = clip;
        musicSource.volume = volume * _musicMult;
        musicSource.Play();
    }
    /// <summary>
    /// Escribir a partir de SoundFx/
    /// </summary>
    public void PlaySoundFX(string path, float volume)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sounds/FX/" + path);
        if (clip == null)
        {
            Debug.LogError("Se intenta reproducir una sonido (SFX) vacio");
            return;
        }
        else
        {
            sfxSource.clip = clip;
            sfxSource.volume = volume * _fxMult;
            sfxSource.PlayOneShot(clip, volume);
        }
    }
    public void PlaySoundFX(string path, float volume, float pitch)
    {
        sfxPitchSource.pitch = 1 + pitch;
        AudioClip clip = Resources.Load<AudioClip>("Sounds/FX" + path);
        if (clip == null)
        {
            Debug.LogError("Se intenta reproducir una sonido (SFX) vacio");
            return;
        }
        else
        {
            sfxPitchSource.clip = clip;
            sfxPitchSource.volume = volume* _fxMult;
            sfxPitchSource.PlayOneShot(clip, volume);
        }
    }
}
