using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal class DayManager : MonoBehaviour
{
  [SerializeField] private Image gameEndPanel;
  [SerializeField] private TMP_Text gameEndText;

  [SerializeField] private Image dayEndPanel;
  [SerializeField] private TMP_Text dayEndText;
  
  [SerializeField] private CanvasGroup transitionPanel;
  [SerializeField] private float transitionDurationSeconds = 4f;
  private readonly WaitForSecondsRealtime wait0_2Seconds = new(0.2f);

  [SerializeField] private float dayDurationSeconds = 300f;
  [SerializeField] private int lastDay = 3;
  private int dayCount = 1;
  internal int CurrentDayIndex => dayCount-1;

  private MessageEventData[] currentDayEvents;
  private int nextEventIndex;
  private float elapsedTime;
  internal float DayProgress => elapsedTime / dayDurationSeconds;

  private int dailyComplaints = 0;
  private int totalComplaints = 0;

  private void Start()
  {
    dayEndPanel.gameObject.SetActive(false);
    gameEndPanel.gameObject.SetActive(false);
    transitionPanel.alpha = 0;
    transitionPanel.gameObject.SetActive(false);
    StartDay();
  }

  private void Update()
  {
    elapsedTime += Time.deltaTime;

    CheckScriptedEvents();

    if (elapsedTime >= dayDurationSeconds)
      EndDay();
  }

  internal void AddComplaint()
  {
    dailyComplaints++;
  }

  private void CheckScriptedEvents()
  {
    while (
      nextEventIndex < currentDayEvents.Length &&
      elapsedTime >= currentDayEvents[nextEventIndex].time)
    {
      MessageEventData messageEvent = currentDayEvents[nextEventIndex];

      GameManager.Instance.messageManager.QueueEvent(messageEvent);

      nextEventIndex++;
    }
  }

  private void StartDay()
  {
    currentDayEvents = GameManager.Instance.MessageDatabase.GetEventsForDay(dayCount);
    nextEventIndex = 0;
    elapsedTime = 0;
    dailyComplaints = 0;

    GameManager.Instance.waterNetwork.Reset();
    AudioManager.Instance.Play("New_Day");
  }

  private void EndDay()
  {
    totalComplaints += dailyComplaints;
    enabled = false;

    if(dayCount == lastDay)
    {
      gameEndText.text = $"Terminaste el juego con {totalComplaints} quejas!";
      gameEndPanel.gameObject.SetActive(true);
      GameManager.Instance.StopTime();

      return;
    }

    dayEndText.text = $"Terminaste el día con {dailyComplaints} quejas!";
    dayEndPanel.gameObject.SetActive(true);
    GameManager.Instance.StopTime();
  }

  public void StartNextDay()
  {
    dayCount++;

    dayEndPanel.gameObject.SetActive(false);
    GameManager.Instance.messageManager.ClearMessages();

    StartCoroutine(PanelTransition());
  }

  private IEnumerator PanelTransition()
  {
    transitionPanel.gameObject.SetActive(true);

    float elapsedTime = 0;
    float halfDuration = transitionDurationSeconds / 2;

    /* Fade in */
    while (elapsedTime < halfDuration)
    {
      elapsedTime += Time.unscaledDeltaTime;
      transitionPanel.alpha = elapsedTime / halfDuration;

      yield return null;
    }

    transitionPanel.alpha = 1;

    yield return wait0_2Seconds;

    StartDay();

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
    GameManager.Instance.ResumeTime();
  }
}
