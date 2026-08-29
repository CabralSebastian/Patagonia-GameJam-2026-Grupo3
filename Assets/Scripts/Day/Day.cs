using UnityEngine;

internal class Day : MonoBehaviour
{
  [SerializeField] private float durationSeconds = 15f;

  private float elapsedTime;

  private void Update()
  {
    elapsedTime += Time.deltaTime;

    if (elapsedTime >= durationSeconds)
      EndDay();
  }

  private void EndDay()
  {
    enabled = false;
    Debug.Log("El día termino!");
  }
}
