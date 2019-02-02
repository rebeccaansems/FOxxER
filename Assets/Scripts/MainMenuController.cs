using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private Vector2 fingerStart;
    private Vector2 fingerEnd;

    private bool diskIsRotating;

    private int[] highScores;

    public float discRotateSpeed;
    public int leftRight = 0, currDirection;

    public GameObject diskObject;
    public CanvasGroup mainCanvas;
    public TextMeshProUGUI currHighscoreText;

    void Start()
    {
        diskIsRotating = false;
        highScores = new int[4] {
            PlayerPrefs.GetInt("Score0", 0),
            PlayerPrefs.GetInt("Score1", 0),
            PlayerPrefs.GetInt("Score3", 0),
            PlayerPrefs.GetInt("Score4", 0)
        };
        currHighscoreText.text = highScores[leftRight].ToString();
    }

    void Update()
    {
        if (diskIsRotating)
        {
            RotateDisk();
            mainCanvas.alpha = 0;
            currHighscoreText.text = highScores[leftRight].ToString();
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

                if ((fingerStart.x - fingerEnd.x) > 80 || Input.GetKey(KeyCode.RightArrow))
                {
                    leftRight++;
                    StartRotatingDisk(1);
                }
                else if ((fingerStart.x - fingerEnd.x) < -80 || Input.GetKey(KeyCode.LeftArrow))
                {
                    leftRight--;
                    StartRotatingDisk(-1);
                }

                fingerStart = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                leftRight = 0;
                fingerStart = Vector2.zero;
                fingerEnd = Vector2.zero;
            }
        }

#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.RightArrow))
        {
            leftRight++;
            StartRotatingDisk(1);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            leftRight--;
            StartRotatingDisk(-1);
        }
#endif

        if (leftRight > 3)
        {
            leftRight = 0;
        }
        else if (leftRight < 0)
        {
            leftRight = 3;
        }
    }

    void StartRotatingDisk(int direction)
    {
        currDirection = direction;
        diskIsRotating = true;
    }

    void RotateDisk()
    {
        diskObject.transform.Rotate(0, currDirection * (discRotateSpeed * Time.deltaTime), 0);

        if ((Mathf.RoundToInt(diskObject.transform.localEulerAngles.y / 2) * 2) % 90 == 0)
        {
            diskIsRotating = false;
            diskObject.transform.localEulerAngles = new Vector3(0, 90 * leftRight, 0);
        }
    }
}
