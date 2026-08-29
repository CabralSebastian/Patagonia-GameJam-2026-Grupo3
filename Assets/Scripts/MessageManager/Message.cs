using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
internal class Message : MonoBehaviour
{
  private Image image;
  [SerializeField] private TMP_Text messageSender;
  [SerializeField] private TMP_Text messageText;
  [SerializeField] private float appearanceTimeSeconds = 3;
  [SerializeField] private float fadingInTimeSeconds = 0.3f;
  [SerializeField] private float fadingOutTimeSeconds = 3;

  private void Start()
  {
    image = GetComponent<Image>();
  }

  internal void SetSender(string sender)
  {
    messageSender.text = sender;
  }

  internal void SetText(string text)
  {
    messageText.text = text;
  }

  internal void SetAlpha(float alpha)
  {
    // image.setAlpha(alpha)
    // messageSender.setAlpha(alpha)
    // messageText.setAlpha(alpha)
  } 
}
