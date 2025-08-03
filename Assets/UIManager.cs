using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject volume;

    private bool _isOnControlScreen;
    private float _fadeDuration = 1f;

    public void startGame(string sceneName)
    {
        //GamePlayUIManager.isPaused = false;
        fadeToScene(sceneName);
    }

    public void resumeGame(string sceneName)
    {
        fadeToScene(sceneName);
    }

    public void exitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void goToControls()
    {
        controls.SetActive(true);
        volume.SetActive(false);
    }
    public void goToVolume()
    {
        controls.SetActive(false);
        volume.SetActive(true);
    }

    private void fadeToScene(string sceneName)
    {
        StartCoroutine(fadeAndLoadScene(sceneName));
    }

    private IEnumerator fadeAndLoadScene(string sceneName)
    {
        // Fade out
        yield return StartCoroutine(fade(1f));

        // Load the new scene
        SceneManager.LoadScene("player_anim");

        // Fade in
        yield return StartCoroutine(fade(0f));
    }

    private IEnumerator fade(float targetAlpha)
    {
        float startAlpha = fadeCanvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < _fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / _fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
    }
}
