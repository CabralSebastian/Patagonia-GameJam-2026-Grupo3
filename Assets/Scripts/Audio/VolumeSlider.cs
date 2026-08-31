using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
  private Slider slider;

  private void Awake()
  {
    slider = GetComponent<Slider>();
  }

  private void Start()
  {
    slider.value = AudioManager.Instance.MasterVolume;
  }

  public void SetMasterVolume(float volume)
  {
    AudioManager.Instance.SetMasterVolume(volume);
  }
}
