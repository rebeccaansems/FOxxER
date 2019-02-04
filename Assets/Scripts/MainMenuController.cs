using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    //private Vector2 fingerStart;
    //private Vector2 fingerEnd;

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
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        selectedLevel = OverallController.instance.currentLevel;
        diskIsRotating = false;
        highScores = new int[4] {
            PlayerPrefs.GetInt("Score0", 0),
            PlayerPrefs.GetInt("Score1", 0),
            PlayerPrefs.GetInt("Score2", 0),
            PlayerPrefs.GetInt("Score3", 0)
        };
        currHighscoreText.text = highScores[selectedLevel].ToString();
        UpdateMusicButtons();

        diskObject.transform.localEulerAngles = new Vector3(0, 90 * selectedLevel, 0);
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

    private Vector2 touchOrigin = -Vector2.one; //start offscreen

    void Gesture()
    {
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
#else
        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.touches[0];

            if (myTouch.phase == TouchPhase.Began)
            {
                touchOrigin = myTouch.position;
            }
            else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
            {
                Vector2 touchEnd = myTouch.position;

                float touchDiffX = touchEnd.x - touchOrigin.x;
                float touchDiffY = touchEnd.y - touchOrigin.y;
                touchOrigin.x = -1;

                if (Mathf.Abs(touchDiffX) > Mathf.Abs(touchDiffY))
                {
                    if (touchDiffX > 0)
                    {
                        selectedLevel++;
                        StartRotatingDisk(1);
                    }
                    else
                    {
                        selectedLevel--;
                        StartRotatingDisk(-1);
                    }
                }
            }
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
        OverallController.instance.currentLevel = selectedLevel;
        SceneManager.LoadScene(1 + selectedLevel);
    }

    public void MuteMusicButtonPressed()
    {
        OverallController.instance.isMuted = !OverallController.instance.isMuted;
        UpdateMusicButtons();
    }

    void UpdateMusicButtons()
    {
        soundOnImage.enabled = !OverallController.instance.isMuted;
        soundOffImage.enabled = OverallController.instance.isMuted;
    }
}
