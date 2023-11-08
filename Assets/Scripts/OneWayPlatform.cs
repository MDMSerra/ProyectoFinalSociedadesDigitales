using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D effector2D;

    public float waitTime;

    private void Start()
    {
        effector2D = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {

        if (Input.GetAxisRaw("Vertical") != -1)
        {
            waitTime = 0.5f;
        }


        if (Input.GetAxisRaw("Vertical") == -1)
        {
            if (waitTime <= 0)
            {
                effector2D.rotationalOffset = 180f;
                waitTime = 0.5f;

            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            effector2D.rotationalOffset = 0;
        }
    }
}
