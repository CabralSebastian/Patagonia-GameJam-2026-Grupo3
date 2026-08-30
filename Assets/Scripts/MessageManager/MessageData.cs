using System;

[Serializable]
internal class MessageData
{
  public  string sender;
  public  string text;
  internal MessageData(string sender, string text)
  {
    this.sender = sender;
    this.text = text;
  }
}
