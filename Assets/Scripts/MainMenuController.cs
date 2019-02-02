using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    private Vector2 fingerStart;
    private Vector2 fingerEnd;

    private bool diskIsRotating;

    private int[] highScores;

    public float discRotateSpeed;
    public int selectedLevel = 0, currDirection;

    public GameObject diskObject;
    public CanvasGroup mainCanvas;
    public TextMeshProUGUI currHighscoreText;
    public Image soundOnImage, soundOffImage;

    private void Awake()
    {
        //setup overall game data
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        diskIsRotating = false;
        highScores = new int[4] {
            PlayerPrefs.GetInt("Score0", 0),
            PlayerPrefs.GetInt("Score1", 0),
            PlayerPrefs.GetInt("Score2", 0),
            PlayerPrefs.GetInt("Score3", 0)
        };
        currHighscoreText.text = highScores[selectedLevel].ToString();
        UpdateMusicButtons();
    }

    void Update()
    {
        if (diskIsRotating)
        {
            RotateDisk();
            mainCanvas.alpha = 0;
            currHighscoreText.text = highScores[selectedLevel].ToString();
        }
        else
        {
            Gesture();
            mainCanvas.alpha = 1;
        }
    }

    void Gesture()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerStart = touch.position;
                fingerEnd = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                fingerEnd = touch.position;

                if ((fingerStart.x - fingerEnd.x) > 40)
                {
                    selectedLevel++;
                    StartRotatingDisk(1);
                }
                else if ((fingerStart.x - fingerEnd.x) < -40)
                {
                    selectedLevel--;
                    StartRotatingDisk(-1);
                }

                fingerStart = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                selectedLevel = 0;
                fingerStart = Vector2.zero;
                fingerEnd = Vector2.zero;
            }
        }

#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.RightArrow))
        {
            selectedLevel++;
            StartRotatingDisk(1);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            selectedLevel--;
            StartRotatingDisk(-1);
        }
#endif
    }

    void StartRotatingDisk(int direction)
    {
        currDirection = direction;
        diskIsRotating = true;

        if (selectedLevel > 3)
        {
            selectedLevel = 0;
        }
        else if (selectedLevel < 0)
        {
            selectedLevel = 3;
        }
    }

    void RotateDisk()
    {
        diskObject.transform.Rotate(0, currDirection * (discRotateSpeed * Time.deltaTime), 0);

        if ((Mathf.RoundToInt(diskObject.transform.localEulerAngles.y / 2) * 2) % 90 == 0)
        {
            diskIsRotating = false;
            diskObject.transform.localEulerAngles = new Vector3(0, 90 * selectedLevel, 0);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1 + selectedLevel);
    }

    public void MuteMusicButtonPressed()
    {
        Debug.Log(OverallController.instance.isMuted);
        OverallController.instance.isMuted = !OverallController.instance.isMuted;
        UpdateMusicButtons();
    }

    void UpdateMusicButtons()
    {
        soundOnImage.enabled = !OverallController.instance.isMuted;
        soundOffImage.enabled = OverallController.instance.isMuted;
    }
}
