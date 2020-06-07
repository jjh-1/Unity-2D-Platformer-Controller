using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    public PlatformerMotor2D motor; //인스펙터로 플레이어꺼만 지정

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<RatController>().killMe();

            //적밟으면 반동으로 튀어오르는 피드백
            motor.ForceJump();
        }
    }
}
