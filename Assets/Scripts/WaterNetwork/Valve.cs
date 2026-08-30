using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

[RequireComponent(typeof(Image))]
internal class Valve : WaterNode, IPointerClickHandler
{
  private Image image;
  [SerializeField] private Sprite greenLightSprite;
  [SerializeField] private Sprite redLightSprite;
  [SerializeField] private WaterNode[] enablingNode;
  private bool isOpen;
  internal override bool IsOpen => isOpen;

  private void Start()
  {
    image = GetComponent<Image>();
    isOpen = false;
    UpdateImage();
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
    UpdateImage();
  }

  internal override void Close()
  {
    isOpen = false;
    UpdateImage();
  }

  private bool CanOpen() => 
    enablingNode.Any(enablingNode => GameManager.Instance.waterNetwork.IsConnected(enablingNode)) &&
    GameManager.Instance.waterNetwork.CanOpenValve;

  private void UpdateImage()
  {
    if(IsOpen)
      image.sprite = greenLightSprite;
    else
      image.sprite = redLightSprite;
  }
}
