using UnityEngine;
using System;

internal class AudioManager : MonoBehaviour
{
  internal static AudioManager Instance;
  [Range(0f, 1f)]
  [SerializeField] private float masterVolume = 1f;
  [SerializeField] internal Sound[] Sounds;

  internal float MasterVolume => masterVolume;

  internal void Awake()
  {
    if(Instance == null)
      Instance = this;
    else
    {
      Destroy(gameObject);
      return;
    }

    DontDestroyOnLoad(gameObject);

    foreach (Sound sound in Sounds)
    {
      sound.Source = gameObject.AddComponent<AudioSource>();
      sound.Source.clip = sound.Clip;
      sound.Source.volume = sound.Volume * masterVolume;
      // sound.Source.pitch = sound.Pitch;
      sound.Source.loop = sound.Loop;
    }
  }

  internal void Start()
  {
    Play("Main_Theme");
  }

  internal void Play(string name)
  {
    Sound sound = Array.Find(Sounds, sound => sound.Name == name);

    sound.Source.Play();
  }

  internal void SetMasterVolume(float volume)
  {
    masterVolume = volume;

    foreach (Sound sound in Sounds)
      sound.Source.volume = sound.Volume * masterVolume;
  }
}
