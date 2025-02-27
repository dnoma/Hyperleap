using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : Singleton<UiManager>
{
    [SerializeField] GameObject mainMenu, Loading, GamePlay;
    [SerializeField] GameObject levelFailPanel;

    [SerializeField] TextMeshProUGUI currentScoreTextUi;
    [SerializeField] TextMeshProUGUI totalScoreTextUi;

    protected override void Awake()
    {
        base.Awake();

        ResetGameUI();

    }

    private void ResetGameUI()
    {
        UpdateCurrentScore(0);
    }

    #region MenuFunctions

    #region MainMenu

    public void StartGame()
    {
        StartCoroutine(LoadingScene());

        SoundManager.instance.PlaySound(SoundManager.instance.buttonClick);
    }

    public void ResethighScore()
    {
        GameManager.instance.ResetHighScore();
        SoundManager.instance.PlaySound(SoundManager.instance.buttonClick);

    }

    #endregion

    #region Loading

    public float fadeDuration = 1f;

    IEnumerator LoadingScene()
    {
        mainMenu.SetActive(false);
        Loading.SetActive(true);

        // Load the loading scene
        AsyncOperation loadingScene = SceneManager.LoadSceneAsync("Loading");
        yield return new WaitUntil(() => loadingScene.isDone);

        // Start fading in the UI
        yield return StartCoroutine(FadeIn());

        // Load the gameplay scene asynchronously
        AsyncOperation gameplayLoad = SceneManager.LoadSceneAsync("GamePlay");
        gameplayLoad.allowSceneActivation = false;

        // Wait until loading is nearly complete
        while (gameplayLoad.progress < 0.9f)
        {
            yield return null;
        }

        Loading.SetActive(false);
        GamePlay.SetActive(true);

        // Once the loading is complete, allow scene activation
        gameplayLoad.allowSceneActivation = true;
        SoundManager.instance.PlayBG();
    }

    IEnumerator FadeIn()
    {
        var fadeImage = Loading.GetComponent<Image>();
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.gameObject.SetActive(true);

        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeImage.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;
    }

    public void BackToMainMenu()
    {
        Loading.SetActive(false);
        mainMenu.SetActive(true);


        StopAllCoroutines();
        SceneManager.LoadSceneAsync("MainMenu");

        SoundManager.instance.PasueBg();
        SoundManager.instance.PlaySound(SoundManager.instance.buttonClick);

        UpdateTotalScore(GameManager.instance.gameConstants.highScore);

    }

    #endregion

    #region GamePlay

    public void PasueGame(bool state)
    {
        Time.timeScale = state ? 1f : 0f;

        if(!state) 
            SoundManager.instance.PasueBg();

        else
            SoundManager.instance.PlayBG();

        SoundManager.instance.PlaySound(SoundManager.instance.buttonClick);

    }

    public void UpdateCurrentScore(int score)
    {
        currentScoreTextUi.text = "SCORE: " + score;
    }
    
    public void UpdateTotalScore(int score)
    {
        totalScoreTextUi.text = "TOP- " + score;
    }

    public void ShowLevelFail()
    {
        levelFailPanel.SetActive(true);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;

        GamePlay.SetActive(false);
        mainMenu.SetActive(true);

        SceneManager.LoadSceneAsync("MainMenu");
        SoundManager.instance.PasueBg();

        SoundManager.instance.PlaySound(SoundManager.instance.buttonClick);
    }

    public void ResetGame() { Time.timeScale = 1f; GameManager.instance.ResetGame(); SoundManager.instance.PlaySound(SoundManager.instance.buttonClick); }

    #endregion

    #endregion



}
