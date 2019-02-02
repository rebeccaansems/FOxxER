using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{

    private Vector2 fingerStart;
    private Vector2 fingerEnd;

    private bool discIsRotating;

    public float discRotateSpeed;
    public int leftRight = 0, currDirection;
    public GameObject diskObject;

    void Start()
    {
        discIsRotating = false;
    }

    void Update()
    {
        if (discIsRotating)
        {
            diskObject.transform.Rotate(0, currDirection * (discRotateSpeed * Time.deltaTime), 0);

            if ((Mathf.RoundToInt(diskObject.transform.localEulerAngles.y / 2) * 2) % 90 == 0)
            {
                discIsRotating = false;
                diskObject.transform.localEulerAngles = new Vector3(0, 90 * leftRight, 0);
            }
        }
        else
        {
            Gesture();
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
                    RotateDisk(1);
                }
                else if ((fingerStart.x - fingerEnd.x) < -80 || Input.GetKey(KeyCode.LeftArrow))
                {
                    leftRight--;
                    RotateDisk(-1);
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
            RotateDisk(1);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            leftRight--;
            RotateDisk(-1);
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

    void RotateDisk(int direction)
    {
        currDirection = direction;
        discIsRotating = true;
    }
}
