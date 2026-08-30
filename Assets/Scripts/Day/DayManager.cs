using System.Collections;
using UnityEngine;
using UnityEngine.UI;

internal class DayManager : MonoBehaviour
{
  [SerializeField] private Image gameEndPanel;
  [SerializeField] private Image dayEndPanel;
  [SerializeField] private CanvasGroup transitionPanel;
  [SerializeField] private float transitionDuration = 2f;
  private readonly WaitForSecondsRealtime wait0_2Seconds = new(0.2f);

  [SerializeField] private float durationSeconds = 15f;
  [SerializeField] private int lastDay = 3;
  private int dayCount = 1;

  private float elapsedTime;

  private void Start()
  {
    dayEndPanel.gameObject.SetActive(false);
    gameEndPanel.gameObject.SetActive(false);
    transitionPanel.alpha = 0;
    transitionPanel.gameObject.SetActive(false);
  }

  private void Update()
  {
    elapsedTime += Time.deltaTime;

    if (elapsedTime >= durationSeconds)
      EndDay();
  }

  private void EndDay()
  {
    enabled = false;

    if(dayCount == lastDay)
    {
      Debug.Log("El juego termino");
      gameEndPanel.gameObject.SetActive(true);
      GameManager.Instance.Pause();

      return;
    }

    Debug.Log("El día termino!");
    dayEndPanel.gameObject.SetActive(true);

    GameManager.Instance.Pause();
  }

  public void StartNextDay()
  {
    dayCount++;
    elapsedTime = 0;
    dayEndPanel.gameObject.SetActive(false);

    GameManager.Instance.messageManager.ClearMessages();

    StartCoroutine(PanelTransition());
  }

  private IEnumerator PanelTransition()
  {
    transitionPanel.gameObject.SetActive(true);

    float elapsedTime = 0;
    float halfDuration = transitionDuration / 2;

    /* Fade in */
    while (elapsedTime < halfDuration)
    {
      elapsedTime += Time.unscaledDeltaTime;
      transitionPanel.alpha = elapsedTime / halfDuration;

      yield return null;
    }

    transitionPanel.alpha = 1;

    yield return wait0_2Seconds;

    /* Fade out */
    elapsedTime = halfDuration;

    while (elapsedTime > 0)
    {
      elapsedTime -= Time.unscaledDeltaTime;
      transitionPanel.alpha = elapsedTime / halfDuration;

      yield return null;
    }

    transitionPanel.alpha = 0;
    transitionPanel.gameObject.SetActive(false);
    enabled = true;
    GameManager.Instance.Unpause();
  }
}
