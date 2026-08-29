using UnityEngine;

internal class GameManager : MonoBehaviour
{
  internal static GameManager Instance;
  [SerializeField] internal WaterNetwork waterNetwork;

  private void Awake()
  {
    transform.SetParent(null);
    if (Instance && Instance != this)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;
    DontDestroyOnLoad(gameObject);
  }
}
