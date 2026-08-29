using System.Collections;
using UnityEngine;
using UnityEngine.UI;

internal class DayManager : MonoBehaviour
{
  [SerializeField] private Image endDayPanel;
  [SerializeField] private CanvasGroup transitionPanel;
  [SerializeField] private float transitionDuration = 2f;
  private readonly WaitForSeconds wait0_2Seconds = new(0.2f);

  [SerializeField] private float durationSeconds = 15f;
  [SerializeField] private int lastDay = 3;
  private int dayCount = 1;

  private float elapsedTime;

  private void Start()
  {
    endDayPanel.gameObject.SetActive(false);
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
      //TODO: Mostrar panel de final de juego.

      return;
    }

    Debug.Log("El día termino!");
    endDayPanel.gameObject.SetActive(true);

    //TODO: Pausar juego.
  }

  public void StartNextDay()
  {
    dayCount++;
    elapsedTime = 0;
    endDayPanel.gameObject.SetActive(false);

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
      elapsedTime += Time.deltaTime;
      transitionPanel.alpha = elapsedTime / halfDuration;

      yield return null;
    }

    transitionPanel.alpha = 1;

    yield return wait0_2Seconds;

    /* Fade out */
    elapsedTime = halfDuration;

    while (elapsedTime > 0)
    {
      elapsedTime -= Time.deltaTime;
      transitionPanel.alpha = elapsedTime / halfDuration;

      yield return null;
    }

    transitionPanel.alpha = 0;
    transitionPanel.gameObject.SetActive(false);
    enabled = true;
    //TODO: Despausar juego.
  }
}
