using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{
    private PlatformerMotor2D motor;

    void Start()
    {
        motor = GetComponent<PlatformerMotor2D>();
        
        //첫상태셋
        motor.normalizedXMovement = -1;
    }

    // Update is called once per frame
    void Update()
    {
        //좌우이동, 벽만나면 방향전환
        //대략 그라운드이넘이 01 이고 레프트월이넘이 10 이라면 두개 or 연산하면 11로 그라운드고 레프트월인걸 알수있다
        if ((motor.facingLeft && motor.collidingAgainst == (PlatformerMotor2D.CollidedSurface.Ground | PlatformerMotor2D.CollidedSurface.LeftWall))
            || (!motor.facingLeft && motor.collidingAgainst == (PlatformerMotor2D.CollidedSurface.Ground | PlatformerMotor2D.CollidedSurface.RightWall)))
        {
            motor.normalizedXMovement *= -1;
            transform.localScale = new Vector3(transform.localScale.x * -1, 1, 1);
        }

    }
}
