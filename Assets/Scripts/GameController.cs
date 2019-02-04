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
    private CanvasGroup pauseScreen, restartScreen;
    [SerializeField]
    private Image jumpZone;

    private int currentLevel = 0;


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
        prevHighScoreText.text = PlayerPrefs.GetInt("Score" + currentLevel, 0).ToString();

        UpdateMusicButtons();
    }

    private void Update()
    {
        currentGameScoreText.text = gameScore.ToString();
    }
    public void GameOver()
    {
        Time.timeScale = 0;

        restartScreen.interactable = true;
        restartScreen.blocksRaycasts = true;
        restartScreen.alpha = 1;

        jumpZone.raycastTarget = false;

        PlayerPrefs.SetInt("Score" + currentLevel, Mathf.Max(PlayerPrefs.GetInt("Score" + currentLevel, 0), gameScore));

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
        foreach(GameObject soundOn in GameObject.FindGameObjectsWithTag("Sound On"))
        {
            soundOn.GetComponent<Image>().enabled = !OverallController.instance.isMuted;
        }

        foreach (GameObject soundOff in GameObject.FindGameObjectsWithTag("Sound Off"))
        {
            soundOff.GetComponent<Image>().enabled = OverallController.instance.isMuted;
        }
    }

}
