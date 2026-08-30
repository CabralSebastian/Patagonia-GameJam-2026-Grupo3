using System;

[Serializable]
internal class ScriptedMessageData
{
  public MessageEventData[] events;
}

[Serializable]
internal class MessageEventData
{
  public int day;
  public float time;
  public MessageData[] messages;
}