using System.Linq;
using UnityEngine;

internal class MessageDatabase
{
  private ComplaintData complaints;
  private ScriptedMessageData scriptedMessages;

  internal void Load()
  {
    TextAsset json = Resources.Load<TextAsset>("Messages/complaints");
    complaints = JsonUtility.FromJson<ComplaintData>(json.text);

    TextAsset scriptedMessagesJson = Resources.Load<TextAsset>("Messages/scripted");
    scriptedMessages = JsonUtility.FromJson<ScriptedMessageData>(scriptedMessagesJson.text);
  }

  internal string GetRandomNoWaterComplaint()
  {
    int index = Random.Range(0, complaints.noWater.Length);
    return complaints.noWater[index];
  }

  internal MessageEventData[] GetEventsForDay(int day)
  {
    return scriptedMessages.events
      .Where(messageEvent => messageEvent.day == day)
      .OrderBy(messageEvent => messageEvent.time)
      .ToArray();
  }
}