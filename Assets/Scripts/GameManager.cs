using UnityEngine;

internal class GameManager : MonoBehaviour
{
  internal static GameManager Instance;
  [SerializeField] internal WaterNetwork waterNetwork;
  [SerializeField] internal MessageManager messageManager;

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
