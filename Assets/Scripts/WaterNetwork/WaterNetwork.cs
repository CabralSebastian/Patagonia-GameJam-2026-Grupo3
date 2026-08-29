using System.Collections.Generic;
using UnityEngine;

internal class WaterNetwork : MonoBehaviour
{
  [SerializeField] private WaterNode waterSource;
  [SerializeField] private WaterNode[] valves;

  private readonly HashSet<WaterNode> connectedValves = new();

  private void Start()
  {
    connectedValves.Add(waterSource);
  }

  internal bool IsConnected(WaterNode valve)
  {
    return connectedValves.Contains(valve);
  }

  internal void Recalculate()
  {
    connectedValves.Clear();

    Queue<WaterNode> pending = new();
    pending.Enqueue(waterSource);
    connectedValves.Add(waterSource);

    while (pending.Count > 0)
    {
      WaterNode current = pending.Dequeue();

      foreach (WaterNode valve in valves)
      {
        if (!valve.IsOpen)
          continue;

        if (!valve.IsEnabledBy(current))
          continue;

        if (connectedValves.Contains(valve))
          continue;

        connectedValves.Add(valve);
        pending.Enqueue(valve);
      }
    }

    foreach (WaterNode valve in valves)
    {
      if (valve.IsOpen && !connectedValves.Contains(valve))
        valve.Close();
    }
  }
}
