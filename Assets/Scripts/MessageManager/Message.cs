using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
internal class Message : MonoBehaviour
{
  private CanvasGroup canvasGroup;
  [SerializeField] private TMP_Text messageSender;
  [SerializeField] private TMP_Text messageText;

  private void Awake()
  {
    canvasGroup = GetComponent<CanvasGroup>();
    canvasGroup.alpha = 0;
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
    canvasGroup.alpha = alpha;
  } 
}
