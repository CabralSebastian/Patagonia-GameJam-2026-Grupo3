using UnityEngine;

internal abstract class WaterNode : MonoBehaviour
{
  internal abstract bool IsOpen { get; }
  internal abstract bool IsEnabledBy(WaterNode waterNode);
  internal abstract void Close();
}
