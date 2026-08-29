using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

[RequireComponent(typeof(Image))]
internal class Valve : WaterNode, IPointerClickHandler
{
  private Image image;
  [SerializeField] private WaterNode[] enablingNode;
  private bool isOpen;
  internal override bool IsOpen => isOpen;

  private void Start()
  {
    image = GetComponent<Image>();
    isOpen = false;
    UpdateImageColor();
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    if (IsOpen)
    {
      Close();
      GameManager.Instance.waterNetwork.Recalculate();
      return;
    }

    if (CanOpen())
    {
      Open();
      GameManager.Instance.waterNetwork.Recalculate();
    }
    else
      Debug.Log("No puedo abrir esta válvula"); // TODO: Trigger sound
  }

  internal override bool IsEnabledBy(WaterNode waterNode) => 
    enablingNode.Any(enablingNode => enablingNode == waterNode);

  internal void Open()
  {
    isOpen = true;
    UpdateImageColor();
  }

  internal override void Close()
  {
    isOpen = false;
    UpdateImageColor();
  }

  private bool CanOpen() => 
    enablingNode.Any(enablingNode => GameManager.Instance.waterNetwork.IsConnected(enablingNode));

  private void UpdateImageColor()
  {
    if(IsOpen)
      image.color = Color.green;
    else
      image.color = Color.red;
  }
}
