using MalbersAnimations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoxController : MonoBehaviour
{
    public CameraFollow cameraFollow;

    private GameObject currentIsland;

    private int zCounter, zMatchCounter;
    private int lastZvalue;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Island") && currentIsland != other.gameObject)
        {
            currentIsland = other.gameObject;
            GameController.instance.gameScore++;

            this.GetComponent<Animal>().trotSpeed.animator = Mathf.Min((GameController.instance.gameScore * 0.005f) + 1, 2);
        }
    }

    void Update()
    {
        if (Time.timeScale == 1)
        {
            if (IfStuck())
            {
                Die();
            }

            if (this.transform.position.y < -0.6f)
            {
                Die();
            }
            else if (Mathf.Abs(this.transform.position.x) > 0.05f)
            {
                float step = 0.05f * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, transform.position.y, transform.position.z), step);
            }
        }
    }

    private void Die()
    {
        cameraFollow.cameraFollowOn = false;
        this.GetComponent<MalbersInput>().AlwaysForward = false;
        this.GetComponent<StepsManager>().Active = false;
        this.GetComponent<Animal>().getDamaged(new DamageValues(Vector3.up, 200));

        StartCoroutine(Restart());
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(3);
        GameController.instance.GameOver();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private bool IfStuck()
    {
        if (zCounter > 60)
        {
            zCounter = 0;
            if ((int)this.transform.position.z == lastZvalue)
            {
                zMatchCounter++;
                if (zMatchCounter > 5)
                {
                    return true;
                }
            }
            else
            {
                zMatchCounter = 0;
            }
            lastZvalue = (int)this.transform.position.z;
        }
        zCounter++;
        return false;
    }
}
