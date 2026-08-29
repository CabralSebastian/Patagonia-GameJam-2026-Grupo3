internal class MessageData
{
  internal readonly string Sender;
  internal readonly string Text;
  internal MessageData(string sender, string text)
  {
    Sender = sender;
    Text = text;
  }
}
