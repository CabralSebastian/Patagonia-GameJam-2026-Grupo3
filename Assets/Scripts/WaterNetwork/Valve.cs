using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[RequireComponent(typeof(Image))]
internal class Valve : WaterNode
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

  internal void Interact()
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
      AudioManager.Instance.Play("Cant_Open_Valve");
  }

  internal override bool IsEnabledBy(WaterNode waterNode) => 
    enablingNode.Any(enablingNode => enablingNode == waterNode);

  internal void Open()
  {
    isOpen = true;
    UpdateImage();
    AudioManager.Instance.Play("Open_Valve");
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
