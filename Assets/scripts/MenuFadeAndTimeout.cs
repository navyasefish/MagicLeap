using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MenuFadeManager : MonoBehaviour
{
    public CanvasGroup menuCanvasGroup;
    public float fadeDuration = 0.5f;
    public float inactivityTime = 10f;

    private float lastActivityTime;
    private bool isVisible = true;
    private int tapCount = 0;
    private float doubleTapThreshold = 0.4f;

    void Start()
    {
        if (menuCanvasGroup == null)
            menuCanvasGroup = GetComponent<CanvasGroup>();

        ShowMenuInstant();
        ResetTimer();
    }

    void Update()
    {
        // Simulated activity detection — spacebar, click, or gesture input
        if (Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
        {
            RegisterActivity();
        }

        // Check for double tap (spacebar here, can change to real input later)
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            tapCount++;
            CancelInvoke("ResetTap");
            Invoke("ResetTap", doubleTapThreshold);

            if (tapCount == 2)
            {
                tapCount = 0;
                if (!isVisible)
                    StartCoroutine(FadeIn());
            }
        }

        if (isVisible && Time.time - lastActivityTime >= inactivityTime)
        {
            StartCoroutine(FadeOut());
        }
    }

    void RegisterActivity()
    {
        if (!isVisible)
            return;

        ResetTimer();
    }

    void ResetTimer()
    {
        lastActivityTime = Time.time;
    }

    void ResetTap()
    {
        tapCount = 0;
    }

    IEnumerator FadeOut()
    {
        isVisible = false;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            menuCanvasGroup.alpha = Mathf.Lerp(1, 0, elapsed / fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        menuCanvasGroup.alpha = 0;
        menuCanvasGroup.interactable = false;
        menuCanvasGroup.blocksRaycasts = false;
    }

    IEnumerator FadeIn()
    {
        isVisible = true;
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            menuCanvasGroup.alpha = Mathf.Lerp(0, 1, elapsed / fadeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        menuCanvasGroup.alpha = 1;
        menuCanvasGroup.interactable = true;
        menuCanvasGroup.blocksRaycasts = true;
        ResetTimer();
    }

    void ShowMenuInstant()
    {
        menuCanvasGroup.alpha = 1;
        menuCanvasGroup.interactable = true;
        menuCanvasGroup.blocksRaycasts = true;
    }
}
