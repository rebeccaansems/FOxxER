using MalbersAnimations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoxController : MonoBehaviour
{
    public float startSpeed = 1, topSpeed = 2;

    private List<GameObject> islandsHit = new List<GameObject>();

    private int zCounter, zMatchCounter;
    private int lastZvalue;
    private bool hasHitWater = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Island") && islandsHit.Contains(other.gameObject) == false)
        {
            islandsHit.Add(other.gameObject);
            GameController.instance.gameScore++;

            this.GetComponent<PlayAudio>().Play(0);
            this.GetComponent<Animal>().trotSpeed.animator = Mathf.Min((GameController.instance.gameScore * 0.005f) + startSpeed, topSpeed);
        }
        else if (other.gameObject.CompareTag("Water") && hasHitWater == false) //play drowning noise
        {
            this.GetComponent<PlayAudio>().Play(3);
            Die();
            hasHitWater = true;
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
        Camera.main.GetComponent<CameraFollow>().cameraFollowOn = false;
        this.GetComponent<MalbersInput>().AlwaysForward = false;
        this.GetComponent<StepsManager>().Active = false;
        this.GetComponent<Animal>().getDamaged(new DamageValues(Vector3.up, 200));

        StartCoroutine(Restart());
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(1);
        GameController.instance.GameOver();
    }

    private bool IfStuck()
    {
        if (zCounter > 60)
        {
            zCounter = 0;
            if ((int)this.transform.position.z == lastZvalue)
            {
                zMatchCounter++;
                if (zMatchCounter > 2)
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
