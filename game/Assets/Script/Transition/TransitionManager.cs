using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : Singleton<TransitionManager>
{
    [SceneName] public string startScene;

    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration;
    private bool isFade;
    private bool canTransition;


    private void OnEnable()
    {
        EventHandler.GameStateChangeEvent += OnGameStateChangeEvent;
    }

    private void OnDisable()
    {
        EventHandler.GameStateChangeEvent -= OnGameStateChangeEvent;
        
    }

    private void Start()
    {
        StartCoroutine(TransitionToScene(string.Empty, startScene));
        canTransition = true;
    }

    private void OnGameStateChangeEvent(GameState gameState)
    {
        canTransition = gameState == GameState.GamePlay;
    }

    
    public void Transition(string from, string to)
    {
        if(!isFade && canTransition)
        StartCoroutine(TransitionToScene(from, to));
    }

    private IEnumerator TransitionToScene(string from, string to)
    {
        yield return Fade(1);
        if(from != string.Empty)
        {
            EventHandler.CallBeforeSceneUnloadEvent();

            yield return SceneManager.UnloadSceneAsync(from);
        }
        yield return SceneManager.LoadSceneAsync(to, LoadSceneMode.Additive);

        int sceneCount = SceneManager.sceneCount;
        if (sceneCount > 0)
        {
            Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
            SceneManager.SetActiveScene(newScene);
        }

        EventHandler.CallAfterSceneloadEvent();
        yield return Fade(0);
    }

    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;
        fadeCanvasGroup.blocksRaycasts = true;

        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;

        while(! Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }

        fadeCanvasGroup.blocksRaycasts = false;

        isFade = false;
    }
}

