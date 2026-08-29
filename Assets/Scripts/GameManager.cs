using UnityEngine;
using UnityEngine.SceneManagement;

internal class GameManager : MonoBehaviour
{
  internal static GameManager Instance;
  [SerializeField] private string MainMenuSceneName = "MainMenu";
  [SerializeField] internal WaterNetwork waterNetwork;
  [SerializeField] internal MessageManager messageManager;

  private void Awake()
  {
    if (Instance && Instance != this)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;
  }

  public void MainMenu()
  {
    Unpause();
    SceneManager.LoadScene(MainMenuSceneName);
  }

  internal void Pause()
  {
    Time.timeScale = 0f;
  }

  internal void Unpause()
  {
    Time.timeScale = 1f;
  }
}
