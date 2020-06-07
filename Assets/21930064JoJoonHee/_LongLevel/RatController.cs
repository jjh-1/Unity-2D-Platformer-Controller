﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{
    private PlatformerMotor2D motor;

    void Start()
    {
        motor = GetComponent<PlatformerMotor2D>();

        motor.normalizedXMovement = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if ((motor.facingLeft && motor.collidingAgainst == (PlatformerMotor2D.CollidedSurface.Ground | PlatformerMotor2D.CollidedSurface.LeftWall))
            || (!motor.facingLeft && motor.collidingAgainst == (PlatformerMotor2D.CollidedSurface.Ground | PlatformerMotor2D.CollidedSurface.RightWall)))
        {
            motor.normalizedXMovement *= -1;
            transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        }

    }
}
