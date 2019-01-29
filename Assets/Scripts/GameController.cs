using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int gameScore;

    [SerializeField]
    private Text currentGameScoreText;
    [SerializeField]
    private Text prevHighScoreText;


    public static GameController instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
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
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("Score", Mathf.Max(PlayerPrefs.GetInt("Score", 0), gameScore));

        PlayerPrefs.Save();
    }
}
