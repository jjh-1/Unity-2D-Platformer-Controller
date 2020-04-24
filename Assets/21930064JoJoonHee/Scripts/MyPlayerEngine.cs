using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerEngine : MonoBehaviour
{
    // 디버그용 기즈모들 그릴건가 불리언
    public bool drawDebugGizmo = true;
    //-------------------------------------------------------
    // 체크할 레이어들 마스크(목록)
    public LayerMask CheckerMask;
    // ? 레이캐스트 체크 거리 ?
    public float CheckerDist = 1;
    //-------------------------------------------------------
    // 땅 가속도 변수
    public float groundAccel = 100;
    // 땅 최대속도 변수
    public float groundMaxSpd = 500;
    //-------------------------------------------------------
    // 공중 가속도 변수
    public float airAccel = 100;
    // 공중 최대속도 변수
    public float airMaxSpd = 500;
    
    // 낙하 최대속도 변수
    public float fallMaxSpd = 100;
    //-------------------------------------------------------
    // 점프 높이 변수
    public float jumpHght = 16;
    // 더블점프 불리언
    public bool canDoubleJump = true;
    //-------------------------------------------------------
    // 벽 붙기 불리언
    public bool canWallStick = true;
    // 벽 슬라이딩 불리언
    public bool willWallSilde = false;
    // 벽 점프 불리언
    public bool canWallJump = true;
    //-------------------------------------------------------
    // 대쉬 불리언
    public bool canDash = true;
    // 대쉬 쿨타임 변수
    public float dashCoolTime = 1;
    // 대쉬 속도 변수
    public float dashSpd = 500;
    // 대쉬 지속시간 변수
    public float dashDuration = 0.2f;

    //=======================================================

    // FSM용 이넘 (개인적인 명확성 위해 문법무시) 
    public enum EngineState
    {
        Grounding,
        Jumping,
        Falling,
        Walling,
        Cornering,
        Dashing
    }
    private EngineState engineState;
    //-------------------------------------------------------
    // 주변 맞닿은 상황용 이넘 (역시 문법무시)
    public enum Surfacing
    {
        None,
        Ground,
        LeftWall,
        RightWall
    }
    private Surfacing surfacing;

    //=======================================================



    //=======================================================

    // 방향 벡터 변수 셋 메소드
    private Vector2 moveDir;
    public void SetMoveDir(Vector2 moveDir)
    {
        this.moveDir = moveDir;
    }
    //-------------------------------------------------------
    // 점프한 순간의 도약 시작 상태셋팅 메소드
    public void Jump()
    {
        engineState = EngineState.Jumping;
    }
    //-------------------------------------------------------
    // @변경@ 점프 버튼 떼면 상승 멈추게 할 상태셋팅 메소드
    public void JumpStop()
    {
        engineState = EngineState.Falling;
    }
    //-------------------------------------------------------
    // 버튼 누르면 대쉬하는 대쉬 상태셋팅 메소드
    public void Dash()
    {
        engineState = EngineState.Dashing;
    }
    //-------------------------------------------------------
    // 애니메이션 스크립트에 상태 넘겨줄 메소드 (상태 보호)
    public EngineState GetEngineState()
    {
        return engineState;
    }
    // 애니메이션 스크립트에 방향 넘겨줄 메소드 (상태 보호)
    public bool IsFacingRight()
    {
        return moveDir.x > 0 ? true : false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
