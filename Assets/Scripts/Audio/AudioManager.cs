using UnityEngine;
using System;

internal class AudioManager : MonoBehaviour
{
  internal static AudioManager Instance;
  [SerializeField] internal Sound[] Sounds;

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
      sound.Source.volume = sound.Volume;
      sound.Source.pitch = sound.Pitch;
      sound.Source.loop = sound.Loop;
    }
  }

  internal void Play(string name)
  {
    Sound sound = Array.Find(Sounds, sound => sound.Name == name);

    sound.Source.Play();
  }
}
