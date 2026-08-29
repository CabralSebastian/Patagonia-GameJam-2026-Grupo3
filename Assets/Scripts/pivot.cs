using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Linq;

[RequireComponent(typeof(Image))]
internal class pivot : MonoBehaviour, IPointerClickHandler
{
  private Image image;
  [SerializeField] private pivot[] enablingValves;
  internal event Action OnClose;

  internal bool IsOpen { get; private set; }
  private bool IsReceivingWater => enablingValves.Any(enablingValve => enablingValve.IsReceivingWater);

  private void Start()
  {
    image = GetComponent<Image>();
    IsOpen = false;
    UpdateImageColor();

    foreach (pivot valve in enablingValves)
      valve.OnClose += CloseCheck;
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    if (IsOpen)
    {
      Close();
      return;
    }

    if (CanOpen())
      Open();
    else
      Debug.Log("No puedo abrir esta válvula"); // TODO: Trigger sound
  }

  private void Open()
  {
    IsOpen = true;
    UpdateImageColor();
  }

  private void Close()
  {
    IsOpen = false;
    UpdateImageColor();
    OnClose?.Invoke();
  }

  private bool CanOpen()
  {
    if(enablingValves.Length == 0)
      return true;

    foreach (pivot valve in enablingValves)
    {
      if (valve.IsOpen)
        return true;
    }

    return false;
  }

  private void CloseCheck()
  {
    if (!IsOpen || CanOpen())
      return;

    Close();
  }


  private void UpdateImageColor()
  {
    if(IsOpen)
      image.color = Color.green;
    else
      image.color = Color.red;
  }
}
