using System;
using System.Collections;
using UnityEngine;

internal class MessageManager : MonoBehaviour
{
  [SerializeField] private Message message;
  [SerializeField] private float popupTimeSeconds = 0.2f;
  [SerializeField] private float visibleTimeSeconds = 3f;
  [SerializeField] private float fadeoutTimeSeconds = 1f;

  private void SendMessage(string sender, string text)
  {
    message.SetSender(sender);
    message.SetText(text);

    StartCoroutine(MessageLifespan());
  }


  private IEnumerator MessageLifespan()
  {
    /*Popup*/
    float timer = 0;
    while (timer<popupTimeSeconds)
    {
      timer+=Time.captureDeltaTime;
      message.SetAlpha(timer/popupTimeSeconds);

      yield return null;
    }

    message.SetAlpha(1);
    //TODO: Add popup message sound

    /*Visible*/
    timer=0;
    while (timer<visibleTimeSeconds)
    {
      timer+=Time.captureDeltaTime;

      yield return null;
    }

    /*FadeOut*/
    timer=fadeoutTimeSeconds;
    while (timer>0)
    {
      timer+=Time.captureDeltaTime;
      message.SetAlpha(timer/fadeoutTimeSeconds);

      yield return null;
    }
    message.SetAlpha(0);
  }
}
