using MalbersAnimations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
