using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{
    private PlatformerMotor2D motor;

    void Start()
    {
        motor = GetComponent<PlatformerMotor2D>();

        motor.normalizedXMovement = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (motor.velocity.x == 0)
        {
            motor.normalizedXMovement *= -1;
        }
    }
}
