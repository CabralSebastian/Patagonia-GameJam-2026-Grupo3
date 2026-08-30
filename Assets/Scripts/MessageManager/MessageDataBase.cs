using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class MessageDatabase
{
  private ComplaintData complaints;
  private int[] noWaterComplaintsUsage;

  private ScriptedMessageData scriptedMessages;

  internal void Load()
  {
    TextAsset json = Resources.Load<TextAsset>("Messages/complaints");
    complaints = JsonUtility.FromJson<ComplaintData>(json.text);

    noWaterComplaintsUsage = new int[complaints.noWater.Length];

    TextAsset scriptedMessagesJson = Resources.Load<TextAsset>("Messages/scripted");
    scriptedMessages = JsonUtility.FromJson<ScriptedMessageData>(scriptedMessagesJson.text);
  }

  internal string GetRandomNoWaterComplaint()
  {
    int index = GetRandomLowestIndex(noWaterComplaintsUsage);
    noWaterComplaintsUsage[index]++;

    return complaints.noWater[index];
  }

  public static int GetRandomLowestIndex(int[] array)
  {
    if (array == null || array.Length == 0)
    {
      Debug.LogError("El array está vacío o es nulo.");
      return -1;
    }

    int lowestValue = array[0];
    List<int> lowestIndices = new() { 0 };

    for (int i = 1; i < array.Length; i++)
    {
      if (array[i] < lowestValue)
      {
        lowestValue = array[i];
        lowestIndices.Clear();
        lowestIndices.Add(i);
      }
      else if (array[i] == lowestValue)
      {
        lowestIndices.Add(i);
      }
    }

    int randomPosition = Random.Range(0, lowestIndices.Count);
    return lowestIndices[randomPosition];
  }

  internal MessageEventData[] GetEventsForDay(int day)
  {
    return scriptedMessages.events
      .Where(messageEvent => messageEvent.day == day)
      .OrderBy(messageEvent => messageEvent.time)
      .ToArray();
  }
}
