using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    public Image fadeImage;
    public GameObject cutsceneUI;
    public float fadeDuration = 1.5f;
    public float cutsceneDuration = 3f;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(PlayCutscene());
        }
    }

    IEnumerator PlayCutscene()
    {
        Time.timeScale = 0f;

        yield return StartCoroutine(FadeToBlack());

        if (cutsceneUI != null)
            cutsceneUI.SetActive(true);

        yield return new WaitForSecondsRealtime(cutsceneDuration);

        //if (cutsceneUI != null)
        //    cutsceneUI.SetActive(false);

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator FadeToBlack()
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            color.a = Mathf.Clamp01(elapsed / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }

    IEnumerator FadeFromBlack()
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            color.a = 1f - Mathf.Clamp01(elapsed / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }
}
