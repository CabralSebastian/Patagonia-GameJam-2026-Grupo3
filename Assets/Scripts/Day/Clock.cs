using TMPro;
using UnityEngine;

public class Reloj : MonoBehaviour
{
  [SerializeField] private int initialHour = 9;
  [SerializeField] private int initialMinute = 0;
  [SerializeField] private int finalHour = 17;
  [SerializeField] private int finalMinute = 0;

  [SerializeField] private TMP_Text time;

  private void Update()
  {
    DayManager dayManager = GameManager.Instance.DayManager;

    float progress = dayManager.DayProgress;

    int initialTotalMinutes = initialHour * 60 + initialMinute;
    int finalTotalMinutes = finalHour * 60 + finalMinute;

    float currentTotalMinutes = Mathf.Lerp(
      initialTotalMinutes,
      finalTotalMinutes,
      progress
    );

    int currentHour = Mathf.FloorToInt(currentTotalMinutes / 60);
    int currentMinute = Mathf.FloorToInt(currentTotalMinutes % 60);

    time.text = $"{ClockFix(currentHour)}:{ClockFix(currentMinute)}";
  }

  private string ClockFix(int value)
  {
    return value.ToString("00");
  }
}
