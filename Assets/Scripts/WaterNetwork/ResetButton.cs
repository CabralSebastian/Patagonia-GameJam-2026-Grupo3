using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(Image))]
internal class ResetButton : MonoBehaviour, IPointerClickHandler
{
  private Image image;
  [SerializeField] private float frameDuration = 0.03f;
  [SerializeField] private Sprite normalSprite;
  [SerializeField] private Sprite pressedSprite1;
  [SerializeField] private Sprite pressedSprite2;

  private void Start()
  {
    image = GetComponent<Image>();
  }

  public void OnPointerClick(PointerEventData eventData)
  {
    GameManager.Instance.waterNetwork.Reset();
    AudioManager.Instance.Play("Button_Click");
    StartCoroutine(PressAnimation());
  }

  private IEnumerator PressAnimation()
  {
    image.sprite = pressedSprite1;
    yield return new WaitForSecondsRealtime(frameDuration);

    image.sprite = pressedSprite2;
    yield return new WaitForSecondsRealtime(frameDuration);

    image.sprite = normalSprite;
  }
}
