using MalbersAnimations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public int gameScore;
    [HideInInspector]
    public GameObject player;

    [SerializeField]
    private TextMeshProUGUI currentGameScoreText;
    [SerializeField]
    private TextMeshProUGUI prevHighScoreText;
    [SerializeField]
    private CanvasGroup pauseScreen, restartScreen, foxUnlockedScreen;
    [SerializeField]
    private Image jumpZone;
    [SerializeField]
    private ParticleSystem highscoreParticleSystem;

    private int currentLevel = 0, prevHighScore;


    public static GameController instance = null;

    void Awake()
    {
        if (instance == null)
        {
            //setup overall game data
            Application.targetFrameRate = 60;

            instance = this;
            player = GameObject.FindGameObjectWithTag("Player");
            currentLevel = SceneManager.GetActiveScene().buildIndex - 1;
        }
        else if (pauseScreen.interactable)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameScore = 0;
        prevHighScore = PlayerPrefs.GetInt("Score" + currentLevel, 0);
        prevHighScoreText.text = prevHighScore.ToString();

        highscoreParticleSystem.Stop();

        UpdateMusicButtons();
    }

    private void Update()
    {
        currentGameScoreText.text = gameScore.ToString();

        if (gameScore > prevHighScore)
        {
            HighscoreAchieved();
            prevHighScore = gameScore;
        }
    }

    private void HighscoreAchieved()
    {
        int numberOfDigits = gameScore.ToString().Length;
        ParticleSystem.ShapeModule particleShape = highscoreParticleSystem.shape;
        particleShape.position = new Vector3((30 * numberOfDigits) - 158, 0, 0);
        particleShape.scale = new Vector3((15 * numberOfDigits) - 5, 25, 0);

        highscoreParticleSystem.Stop();
        highscoreParticleSystem.Play();

        player.GetComponent<PlayAudio>().Play(1);
    }

    public void GameOver()
    {
        Time.timeScale = 0;

        jumpZone.raycastTarget = false;

        int newTotalScore = PlayerPrefs.GetInt("TotalScore") + gameScore;

        bool newFoxUnlocked = false;

        for (int i = 1; i < OverallController.instance.unlockScores.Length; i++)
        {
            if (newTotalScore >= OverallController.instance.unlockScores[i] &&
                PlayerPrefs.GetInt("TotalScore") < OverallController.instance.unlockScores[i])
            {
                newFoxUnlocked = true;
                break;
            }
        }

        if (newFoxUnlocked)
        {
            foxUnlockedScreen.interactable = true;
            foxUnlockedScreen.blocksRaycasts = true;
            foxUnlockedScreen.alpha = 1;
        }
        else
        {
            ShowRestartPanel();
        }

        PlayerPrefs.SetInt("TotalScore", newTotalScore);
        PlayerPrefs.SetInt("Score" + currentLevel, Mathf.Max(prevHighScore, gameScore));

        PlayerPrefs.Save();
    }

    public void Jump()
    {
        if (Time.timeScale == 1)
        {
            player.GetComponent<Animal>().SetJump();
        }
    }

    public void PauseGame()
    {
        if (pauseScreen.interactable)//Unpause
        {
            jumpZone.raycastTarget = true;

            pauseScreen.interactable = false;
            pauseScreen.blocksRaycasts = false;
            pauseScreen.alpha = 0;

            Time.timeScale = 1;
        }
        else
        {
            jumpZone.raycastTarget = false;

            pauseScreen.interactable = true;
            pauseScreen.blocksRaycasts = true;
            pauseScreen.alpha = 1;

            Time.timeScale = 0.001f;
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void ShowRestartPanel()
    {
        restartScreen.interactable = true;
        restartScreen.blocksRaycasts = true;
        restartScreen.alpha = 1;

        foxUnlockedScreen.interactable = false;
        foxUnlockedScreen.blocksRaycasts = false;
        foxUnlockedScreen.alpha = 0;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MuteMusicButtonPressed()
    {
        OverallController.instance.isMuted = !OverallController.instance.isMuted;
        UpdateMusicButtons();
    }

    void UpdateMusicButtons()
    {
        if (OverallController.instance.isMuted)
        {
            PlayerPrefs.SetInt("Muted", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Muted", 0);
        }

        foreach (GameObject soundOn in GameObject.FindGameObjectsWithTag("Sound On"))
        {
            soundOn.GetComponent<Image>().enabled = !OverallController.instance.isMuted;
        }

        foreach (GameObject soundOff in GameObject.FindGameObjectsWithTag("Sound Off"))
        {
            soundOff.GetComponent<Image>().enabled = OverallController.instance.isMuted;
        }
    }

}
