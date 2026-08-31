using TMPro;
using UnityEngine;

internal class GameInfoUI : MonoBehaviour
{
  [SerializeField] private TMP_Text dayText;
  [SerializeField] private TMP_Text valvesText;

  private void Update()
  {
    WaterNetwork waterNetwork = GameManager.Instance.waterNetwork;
    DayManager dayManager = GameManager.Instance.DayManager;

    valvesText.text =
      $"Válvulas: {waterNetwork.OpenValveCount}/{waterNetwork.MaxOpenValves}";

    dayText.text =
      $"Día: {dayManager.CurrentDayIndex + 1}";
  }
}