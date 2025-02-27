using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameConstants gameConstants;

    public int currentScore;
    public bool isPaused;

    private int highScore;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        currentScore = 0;
        highScore = gameConstants.highScore;
        UiManager.instance.UpdateTotalScore(highScore);
    }


    internal void CoinCollected()
    {
        currentScore++;
        UiManager.instance.UpdateCurrentScore(currentScore);
    }

    public void ResetGame()
    {
        OnGameEnd();

        currentScore = 0;

        UiManager.instance.UpdateCurrentScore(currentScore);
        ReloadScene();
    }

    public void ReloadScene()
    {
        SceneManager.LoadSceneAsync("GamePlay");
    }

    public void OnLevelComplete()
    {
        Invoke("DelayLoadNewLevel", .5f);

    }

    void DelayLoadNewLevel()
    {
        ReloadScene();

    }

    // Either player loss or Restart Game
    public void OnGameEnd()
    {
        UpdateHighScore();
    }

    public void UpdateHighScore()
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            gameConstants.highScore = highScore;
        }
    }

    internal void ResetHighScore()
    {
        gameConstants.highScore = 0;
        highScore = 0;

        UiManager.instance.UpdateTotalScore(highScore);
    }
}

