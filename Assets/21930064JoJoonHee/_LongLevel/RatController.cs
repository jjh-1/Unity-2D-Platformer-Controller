using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{
    public Animator animator;

    public BoxCollider2D boxCollider2D;

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

    public void killMe()
    {
        // 플레이어 죽이는 트리거 비활성화
        boxCollider2D.enabled = false;
        motor.frozen = true;

        animator.SetTrigger("KillMe");

        //죽는 애니메이션 끝난후 오브젝트 삭제
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length-0.2f);
    }
}
