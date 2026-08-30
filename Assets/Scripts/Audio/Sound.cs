using System;
using UnityEngine;

[Serializable]
internal class Sound 
{
  [SerializeField] internal string Name;
  [SerializeField] internal AudioClip Clip;

  [Range(0f, 1f)]
  [SerializeField] internal float Volume;
  // [Range(0.1f, 3f)]
  // [SerializeField] internal float Pitch;
  [SerializeField] internal bool Loop;

  [HideInInspector] internal AudioSource Source;
}
