using MalbersAnimations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoxController : MonoBehaviour
{
    public CameraFollow cameraFollow;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < -0.6f)
        {
            Die();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
