using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class MessageManager : MonoBehaviour
{
  [SerializeField] private Message message;
  [SerializeField] private float popupTimeSeconds = 0.2f;
  [SerializeField] private float visibleTimeSeconds = 3f;
  [SerializeField] private float fadeoutTimeSeconds = 1f;

  private readonly Queue<MessageData> messageQueue = new();
  private bool isDisplayingMessage = false;
  private Coroutine messageCoroutine;

  private void TryDisplayNextMessage()
  {
    if (isDisplayingMessage || messageQueue.Count == 0 || messageCoroutine != null)
      return;

    MessageData newMessage = messageQueue.Dequeue();
    DisplayMessage(newMessage.sender, newMessage.text);
  }

  private void DisplayMessage(string sender, string text)
  {
    message.SetSender(sender);
    message.SetText(text);

    messageCoroutine = StartCoroutine(MessageLifespan());
  }

  internal void QueueComplaint(WaterTank waterTank)
  {
    string complaint = GameManager.Instance.MessageDatabase.GetRandomNoWaterComplaint();
    
    MessageData newComplaint = new(waterTank.Username, complaint);
    messageQueue.Enqueue(newComplaint);

    TryDisplayNextMessage();
  }

  internal void QueueEvent(MessageEventData messageEvent)
  {
    foreach (MessageData message in messageEvent.messages)
      messageQueue.Enqueue(message);

    TryDisplayNextMessage();
  }

  internal void ClearMessages()
  {
    messageQueue.Clear();

    if (messageCoroutine != null)
    {
      StopCoroutine(messageCoroutine);
      messageCoroutine = null;
    }

    isDisplayingMessage = false;
    message.SetAlpha(0);
  }

  private IEnumerator MessageLifespan()
  {
    isDisplayingMessage = true;

    /*Popup*/
    float timer = 0;
    while (timer<popupTimeSeconds)
    {
      timer+=Time.deltaTime;
      message.SetAlpha(timer/popupTimeSeconds);

      yield return null;
    }

    message.SetAlpha(1);
    AudioManager.Instance.Play("Message_Notification");

    /*Visible*/
    timer=0;
    while (timer<visibleTimeSeconds)
    {
      timer+=Time.deltaTime;

      yield return null;
    }

    /*FadeOut*/
    timer=fadeoutTimeSeconds;
    while (timer>0)
    {
      timer-=Time.deltaTime;
      message.SetAlpha(timer/fadeoutTimeSeconds);

      yield return null;
    }
    message.SetAlpha(0);

    isDisplayingMessage = false;
    messageCoroutine = null;
    TryDisplayNextMessage();
  }
}
