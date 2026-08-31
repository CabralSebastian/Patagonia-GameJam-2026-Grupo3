using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

internal class GameManager : MonoBehaviour
{
  internal static GameManager Instance;
  [SerializeField] private string MainMenuSceneName = "MainMenu";
  [SerializeField] internal WaterNetwork waterNetwork;
  [SerializeField] internal MessageManager messageManager;
  [SerializeField] internal DayManager DayManager;
  [SerializeField] internal Image PausePanel;
  private bool isPaused = false;

  internal readonly MessageDatabase MessageDatabase = new();

  private void Awake()
  {
    if (Instance && Instance != this)
    {
      Destroy(gameObject);
      return;
    }

    Instance = this;
    MessageDatabase.Load();
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space) ||
      Input.GetKeyDown(KeyCode.Escape))
    {
      TogglePause();
    }
  }

  private void TogglePause()
  {
    isPaused = !isPaused;
    PausePanel.gameObject.SetActive(isPaused);

    Action toggleTime = isPaused ? StopTime : ResumeTime;
    toggleTime();
  }

  public void MainMenu()
  {
    ResumeTime();
    SceneManager.LoadScene(MainMenuSceneName);
  }

  internal void StopTime()
  {
    Time.timeScale = 0f;
  }

  internal void ResumeTime()
  {
    Time.timeScale = 1f;
  }
}
