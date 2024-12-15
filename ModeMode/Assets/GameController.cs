using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public float gameTime;
    public int enemiesKilled;
    public int totalUpgrades = 0;
    public int waveNumber;
    public Stats playerStats;
    public enum GameStates
    {
        PLAYING,
        PAUSED,
        DEAD
    }
    public GameStates gameState;

    public void PauseGame()
    {
        StartCoroutine(PauseCoroutine());
    }

    IEnumerator PauseCoroutine()
    {
        yield return null;
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            gameState = GameStates.PLAYING;
        }
        else
        {
            Time.timeScale = 0;
            gameState = GameStates.PAUSED;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        playerStats.playerDiedEvent += EndGame;
    }

    private void OnDisable()
    {
        playerStats.playerDiedEvent -= EndGame;
    }


    void EndGame()
    {
        gameState = GameStates.DEAD;
    }
    void Update()
    {
        switch (gameState)
        {
            case GameStates.PLAYING:
                Time.timeScale = 1;
                gameTime += Time.deltaTime;
                waveNumber = WaveManager.Instance.wave;
                break;
            case GameStates.PAUSED:
                break;
            case GameStates.DEAD:
                break;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && gameState != GameStates.DEAD)
        {
            PauseGame();
        }
    }
}
