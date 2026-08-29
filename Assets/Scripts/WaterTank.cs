using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

internal class WaterTank : MonoBehaviour
{
  [SerializeField] private Image waterSprite;
  [SerializeField] private Valve[] enablingValves;

  [SerializeField] private float consumptionRate = 1f;
  [SerializeField] private float maxCapacity = 30f;
  [SerializeField] private float waterLevel;

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
  }

  private void OnEmptyWaterTank()
  {
    Debug.Log("HDP! Me dejaste sin agua!");
  }
}
/*

+70% Agua mandan mensajes lindos

-35% Quejas sin

*/