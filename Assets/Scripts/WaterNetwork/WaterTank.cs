using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

internal class WaterTank : MonoBehaviour
{
  [SerializeField] private Image waterSprite;
  [SerializeField] private Valve[] enablingValves;

  [SerializeField] private string username = "Jhon Doe";
  [SerializeField] private float consumptionRate = 1f;
  [SerializeField] private float maxCapacity = 30f;
  [SerializeField] private float waterLevel;
  [SerializeField] private float complaintCooldown = 10f;
  private Coroutine complaintCoroutine;

  internal string Username => username;
  private bool IsReceivingWater => enablingValves.Any(enablingValve => enablingValve.IsOpen);
  private float FillAmount => waterLevel / maxCapacity;

  private void Start()
  {
    waterLevel = maxCapacity;
  }

  private void Update()
  {
    float dTime = Time.deltaTime;

    if(IsReceivingWater)
      FillWaterTank(dTime);
    else
      ConsumeWater(dTime);

    waterSprite.fillAmount = FillAmount;
  }

  private void ConsumeWater(float dTime)
  {
    waterLevel -= consumptionRate * dTime;
    waterLevel = Math.Max(waterLevel, 0);

    if(waterLevel == 0)
      OnEmptyWaterTank();
  }

  private void FillWaterTank(float dTime)
  {
    waterLevel += 1 * dTime; // TODO: FillingRate
    waterLevel = Math.Min(waterLevel, maxCapacity);

  }

  private void OnEmptyWaterTank()
  {
    complaintCoroutine ??= StartCoroutine(ComplaintLoop());
  }

  private IEnumerator ComplaintLoop()
  {

    while (waterLevel <= 0)
    {
      GameManager.Instance.messageManager.QueueComplaint(this);
      yield return new WaitForSeconds(complaintCooldown);
    }

    complaintCoroutine = null;
  }
}
/*

+70% Agua mandan mensajes lindos

-35% Quejas sin

*/