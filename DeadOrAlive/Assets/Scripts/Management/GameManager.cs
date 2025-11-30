using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public RoundManager roundManager;

    [Header("Title Screen Buttons")]
    public Button startButton;
    public Button howToPlayButton;

    [Header("Game Over Screen Buttons")]
    public Button retryButton;
    public Button quitButton;

    [Header("Screens")]
    public GameObject titleScreen;
    public GameObject gameOverScreen;

    [Header("Conditions")]
    // [SerializeField] private bool isGameOver;
    [SerializeField] private bool gamePaused;
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);

        startButton.onClick.AddListener(StartGame);
        retryButton.onClick.AddListener(ResetGame);
    }

    // Update is called once per frame
    void Update()
    {
        if (roundManager.GetRoundTimeLeft() <= 0 && titleScreen.activeSelf == false)
        {
            GameOver();
        }

        if (gamePaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void StartGame()
    {
        titleScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        gamePaused = false;

        // Starting round
        roundManager.currentRoundNum = 0;
        roundManager.StartNewRound(roundManager.minPeopleToGenerate, roundManager.maxPeopleToGenerate);

        roundManager.AddWantedPersonToPoster();

        roundManager.timerRunning = true;
        roundManager.roundTimeLeft = roundManager.maxTimeToStart;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        roundManager.MakePeopleUnclickable();
        gamePaused = true;
    }

    public void ResetGame()
    {
        roundManager.ResetRounds();
        roundManager.ResetTimerToRoundOne();
        roundManager.ClearPeople();

        StartGame();
    }
}
