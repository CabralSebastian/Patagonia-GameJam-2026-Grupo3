using UnityEngine;

internal class Day : MonoBehaviour
{
  [SerializeField] private float durationSeconds = 300f;

  private float elapsedTime;

  private void Update()
  {
    elapsedTime += Time.deltaTime;

    // ProcessScheduledMessages();

    if (elapsedTime >= durationSeconds)
      EndDay();
  }

  private void EndDay()
  {
    Debug.Log("El día termino!");
  }

}
