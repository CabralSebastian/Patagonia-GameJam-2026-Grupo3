using UnityEngine;
using UnityEngine.SceneManagement;

internal class MainMenu : MonoBehaviour
{
  [SerializeField] private string GameSceneName = "Game";

  public void StartGame()
  {
    SceneManager.LoadScene(GameSceneName);
  }
}
