using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance { get; private set; }

    SpriteRenderer fadeImage;
    [SerializeField] float fadeDuration = 1.0f;

    void Awake()
    {
        
        instance = this;
        fadeImage= gameObject.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            //START FADE
            StartCoroutine(FadeOut());
        }
    }

    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(FadeAndLoad(sceneName));
    }

    IEnumerator FadeAndLoad(string sceneNameTL)
    {
        yield return StartCoroutine(FadeIn());
        SceneManager.LoadScene(sceneNameTL);
    }

    IEnumerator FadeIn()
    {
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = 0;

            for (float i = 0; i < fadeDuration; i += Time.deltaTime * fadeDuration)
            {
                color.a = Mathf.Lerp(0, 1, i / fadeDuration);
                fadeImage.color = color;
                yield return null;
            }

            color.a = 1;
            fadeImage.color = color;
        }
    }

    IEnumerator FadeOut()
    {
        if (fadeImage != null)
        {
            Color color = fadeImage.color;
            color.a = 1;

            for (float i = 0; i < fadeDuration; i += Time.deltaTime * fadeDuration)
            {
                color.a = Mathf.Lerp(1, 0, i / fadeDuration);
                fadeImage.color = color;
                yield return null;
            }

            color.a = 0;
            fadeImage.color = color;
        }
    }

}
