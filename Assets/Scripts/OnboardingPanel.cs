using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
internal class OnboardingPanel : MonoBehaviour
{
  private CanvasGroup canvasGroup;
  [SerializeField] private Button continueButton;
  [SerializeField] private WaitForSecondsRealtime secondsToRead = new(10f);
  [SerializeField] private float fadingTime = 2f;

  private void Start()
  {
    canvasGroup = GetComponent<CanvasGroup>();

    GameManager.Instance.StopTime();
    continueButton.gameObject.SetActive(false);
    
    StartCoroutine(WaitAndEnableButton());
  }

  private IEnumerator WaitAndEnableButton()
  {
    yield return secondsToRead;

    continueButton.gameObject.SetActive(true);
  }

  public void Continue()
  {
    continueButton.interactable = false;
    StartCoroutine(FadingAndStartGame());
  }

  private IEnumerator FadingAndStartGame()
  {
    float elapsedTime = fadingTime;

    /* Fade out */
    while (elapsedTime > 0)
    {
      elapsedTime -= Time.unscaledDeltaTime;
      canvasGroup.alpha = elapsedTime / fadingTime;

      yield return null;
    }

    canvasGroup.alpha = 0;
    GameManager.Instance.ResumeTime();
    gameObject.SetActive(false);
  }
}
