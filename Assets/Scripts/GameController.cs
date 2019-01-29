using MalbersAnimations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int gameScore;

    [SerializeField]
    private Text currentGameScoreText;
    [SerializeField]
    private Text prevHighScoreText;
    [SerializeField]
    private CanvasGroup pauseScreen;

    private FoxController player;


    public static GameController instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<FoxController>();
        }
        else if (pauseScreen.interactable)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameScore = 0;
        prevHighScoreText.text = PlayerPrefs.GetInt("Score", 0).ToString();
    }

    private void Update()
    {
        currentGameScoreText.text = gameScore.ToString();

        if (Input.mousePosition.normalized.y > 0.99f)
        {
            player.GetComponent<MalbersInput>().EnableInput("Jump", false);
        }
        else if (pauseScreen.interactable == false)
        {
            player.GetComponent<MalbersInput>().EnableInput("Jump", true);
        }
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("Score", Mathf.Max(PlayerPrefs.GetInt("Score", 0), gameScore));

        PlayerPrefs.Save();
    }

    public void PauseGame()
    {
        if (pauseScreen.interactable)
        {
            pauseScreen.interactable = false;
            pauseScreen.blocksRaycasts = false;
            pauseScreen.alpha = 0;
            player.GetComponent<MalbersInput>().AlwaysForward = true;
            StartCoroutine(EnableJump());
        }
        else
        {
            pauseScreen.interactable = true;
            pauseScreen.blocksRaycasts = true;
            pauseScreen.alpha = 1;
            player.GetComponent<MalbersInput>().AlwaysForward = false;
        }
    }

    IEnumerator EnableJump()
    {
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<MalbersInput>().EnableInput("Jump", true);
    }
}
