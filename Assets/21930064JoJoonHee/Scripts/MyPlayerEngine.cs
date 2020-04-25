using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
public class MyPlayerEngine : MonoBehaviour
{
    #region 퍼블릭 변수들
    // 디버그용 기즈모들 그릴건가 불리언
    public bool drawDebugGizmo = true;
    //-------------------------------------------------------
    // 체크할 레이어들 마스크(목록)
    public LayerMask CheckerMask;
    // ? 레이캐스트 체크 거리 ?
    public float CheckerDist = 1;

    // 서페이싱 체크 거리
    public float surfacingCheckerDist = 0.5f; 
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
    /// <summary>
    /// @변경:노필요-점프버튼 떼면 멈추게 할것@ 홀드 점프 변수
    ///public float ExtraJumpHeight = 0.5f; 
    /// </summary>
    // 더블점프 불리언
    public bool canDoubleJump = true;
    //-------------------------------------------------------
    // 벽 붙기 불리언
    public bool canWallStick = true;
    // 벽 슬라이딩 불리언
    public bool willWallSilde = false;
    // 벽 점프 불리언
    public bool canWallJump = true;

    /// <summary>
    /// @변경:노필요-벽에 붙었을때 안미끄러지게 할수 있게 할건데 스펠렁키 같은 게임 만들거 아닌이상 필요없을거 같음@ 절벽 모서리 잡기 관련 변수들
    ///public bool AllowCornerGrab = false;
    ///public float CornerHeightCheck = 0.1f;
    ///public float CornerWidthCheck = 0.1f;
    /// </summary>
    //-------------------------------------------------------
    // 대쉬 불리언
    public bool canDash = true;
    // 대쉬 쿨타임 변수
    public float dashCoolTime = 1;
    // 대쉬 속도 변수
    public float dashSpd = 500;
    // 대쉬 지속시간 변수
    public float dashDuration = 0.2f;

    /// <summary> 
    /// @변경:맘에안듬-대쉬시 충돌오류 안나게 꼼수 쓸려는거 같은데 나중에 테스트후 고치지뭐
    ///public bool ChangeLayerDuringDash = false;
    ///public int DashLayer = 0;
    /// </summary>
    #endregion
    //=======================================================
    #region 퍼블릭 이넘들
    // FSM용 이넘 (개인적인 명확성 위해 문법무시) 
    public enum EngineState
    {
        None,
        Grounding,
        // @변경-아래두개로 세분화@
        //InAir, 
        Ascending,
        Falling,
        Walling,
        Cornering,
        Dashing
    }
    private EngineState engineState = EngineState.None;
    //-------------------------------------------------------
    // 주변 맞닿은 상황용 이넘 (역시 문법무시)
    public enum Surfacing
    {
        None,
        Ground,
        LeftWall,
        RightWall
    }
    private Surfacing surfacing = Surfacing.None;
    #endregion
    //=======================================================
    #region 프라이빗 변수들
    // !!!! Time.time 은 해당 프레임이 시작된 시간 유니티 변순데 여기에 대쉬 쿨타임 더하면 다음 대쉬 가능한 시간 
    private float dashCanAgainTime = 0;
    // Time.time 에 dashDuration 더해져 지정될 변수 (대쉬 끝나는 시간)
    private float dashEndTime = 0;
    // 대쉬 벡터
    private Vector2 dashingVector = Vector2.zero;
    #endregion
    //=======================================================
    #region 퍼블릭 메소드들, 관련변수
    // 방향 벡터 셋 메소드
    private Vector2 moveDir;
    public void SetMoveDir(Vector2 moveDir)
    {
        this.moveDir = moveDir;
    }
    //-------------------------------------------------------
    // 바라보는 방향 겟,셋 메소드
    // !!!! 안움직이는 상황이면 바뀌지 않으니(else if) 보고있는 방향 적절히 셋됨 !!!!
    private bool isFacingRight = true;
    public void SetIsFacingRight()
    {
        if (moveDir.x > 0)
        {
            isFacingRight = true;
        }
        else if (moveDir.x < 0)
        {
            isFacingRight = false;
        }    
    }
    // 이건 애니메이션 스크립트에서 사용할 메소드 (보호)
    public bool GetIsFacingRight()
    {
        return isFacingRight;
    }
    //-------------------------------------------------------
    // 컨트롤러 스크립트에서(거기선 퍼포먼스 더좋은 그냥 업데이트 사용, 실제 움직이는 엔진 여기선 픽스드 업데이트 사용) 이 메소드 콜해서 대쉬 버튼 눌렀나
    private bool isDashButtonPressed = false;
    public void Dash()
    {      
        isDashButtonPressed = true;
    }
    /*
    // ? 좌우 이동중 대쉬 했을때 사용할 오버로딩 메소드 ?
    private bool isDashingWhileMoving = false;
    public void Dash(Vector2 dir)
    {
        isDashButtonPressed = true;
        isDashingWhileMoving = false;
    }
    */
    //-------------------------------------------------------
    // 점프한 순간의 도약 시작 상태셋팅 메소드
    private bool isJumpButtonPressed = false;
    public void Jump()
    {
        isJumpButtonPressed = true;
    }
    //-------------------------------------------------------
    // @변경@ 점프 버튼 떼면 상승 멈추게 할 상태셋팅 메소드
    public void JumpStop()
    {
        engineState = EngineState.Falling;
    }
    //-------------------------------------------------------
    // 애니메이션 스크립트에 상태 넘겨줄 메소드 (상태 보호)
    public EngineState GetEngineState()
    {
        return engineState;
    }
    #endregion
    //=======================================================
    #region 프라이빗 메소드들
    // @@@@ 맞닿은것 정보 가져오는 메소드
    private void setSurfacing()
    {
        // 움직이니 계속 업데이트 해야하니 여기서 선언, 인스턴스지정, 정보 계속 가져와야함
        Bounds bound = GetComponent<BoxCollider2D>().bounds;
        Vector2 boundMax = bound.max;
        Vector2 boundMin = bound.min;

        // @1@ 땅 먼저 (땅만) 체크
        // 좌,우, 위는 필요없으니 범위 줄임
        boundMax.x -= surfacingCheckerDist;
        boundMin.x += surfacingCheckerDist;
        boundMax.y -= surfacingCheckerDist;
        // 아래 범위는 늘려야함
        boundMin.y -= surfacingCheckerDist;
        
        // 그 범위안에 다른 콜라이더가 있는지 체크
        Collider2D hit = Physics2D.OverlapArea(boundMin, boundMax, CheckerMask);
        if (hit != null)
        {
            surfacing = Surfacing.Ground;
            print("땅부딫힘");
        }
        else
        {
            surfacing = Surfacing.None;
            print("xxxxxxxxx");
        }

        // @2@ 벽타기 기능 위해 땅위는 아니고 오른쪽이나 왼쪽에 벽이있나 체크
        if (surfacing == Surfacing.None)
        {
            // 오른쪽벽 체크
            if (moveDir.x > 0)
            {
                boundMax = bound.max;
                boundMin = bound.min;
                boundMax.x += surfacingCheckerDist; // 이거만 늘리고 다른건 다 줄여서 바운드 오른쪽만 체크
                boundMin.x += surfacingCheckerDist;
                boundMax.y -= surfacingCheckerDist;
                boundMin.y += surfacingCheckerDist;

                hit = Physics2D.OverlapArea(boundMin, boundMax, CheckerMask);
                if (hit != null)
                {
                    surfacing = Surfacing.RightWall;
                }
            }
            // 왼쪽벽 체크
            else if(moveDir.x < 0) 
            {
                boundMax = bound.max;
                boundMin = bound.min;
                boundMax.x -= surfacingCheckerDist; 
                boundMin.x -= surfacingCheckerDist; // 이거만 늘리고 다른건 다 줄여서 바운드 왼쪽만 체크
                boundMax.y -= surfacingCheckerDist;
                boundMin.y += surfacingCheckerDist;

                hit = Physics2D.OverlapArea(boundMin, boundMax, CheckerMask);
                if (hit != null)
                {
                    surfacing = Surfacing.LeftWall;
                }
            }
        }
    }
    #endregion
    //=======================================================
    #region 모노비헤비어 메소드들, 유니티 인스턴스, 관련 변수들
    // 리지드바디 인스턴스 지정, 관련변수
    private Rigidbody2D rigidbody;
    private float initialDrag;
    private float initialGrav;

    // Start is called before the first frame update
    void Start()
    {
        // 리지드바디 관련 이닛
        rigidbody = GetComponent<Rigidbody2D>();
        initialDrag = rigidbody.drag;
        initialGrav = rigidbody.gravityScale;

    }
    //-------------------------------------------------------
    // Update is called once per frame
    void FixedUpdate()
    {
        SetIsFacingRight();

        // @@@@ [대쉬 버튼 누른순간의 첫 한번 세팅]
        if (canDash
            &&
            isDashButtonPressed
            &&
            (Time.time >= dashCanAgainTime))
        {
            // !!!! Time.time 은 해당 프레임이 시작된 시간 유니티 변순데 여기에 대쉬 쿨타임 더하면 다음 대쉬 가능한 시간 셋
            dashCanAgainTime = Time.time + dashCoolTime;
            // 대쉬 끝나는 시간 셋
            dashEndTime = Time.time + dashDuration;

            // 대쉬 상태 셋
            rigidbody.drag = 0;
            rigidbody.gravityScale = 0;
            engineState = EngineState.Dashing;

            // 대쉬 벡터 지정
            if (isFacingRight)
            {
                dashingVector = Vector2.right * dashSpd;
            }
            else
            {
                dashingVector = Vector2.left * dashSpd;
            }
        }
        isDashButtonPressed = false;

        // @@@@ [실제 대쉬 수행]. 대쉬는 모든 조건 무시하는 최상위 상태
        if (engineState == EngineState.Dashing)
        {
            rigidbody.velocity = dashingVector;

            // 대쉬 첫세팅에서 정한 대쉬 끝나는 시간 넘음 - 대쉬종료
            if (Time.time >= dashEndTime)
            {
                // 원래상태, 원래 리지드바디 변수들로
                engineState = EngineState.None;
                rigidbody.gravityScale = initialGrav;
                // ? 공중에서 드래그 따로하나 ?
                if (surfacing == Surfacing.Ground)
                {
                    rigidbody.drag = initialDrag;
                }
            }

        }
        // 대쉬 외 상태들
        else
        {
            setSurfacing();

            /*
            // !?!? 리지드바디 계속 업데이트 해야하나? 좀되서 기억안나네
            if ((surfacing != Surfacing.None) && (rigidbody.velocity.y < 0))
            {
                // 이즈 점핑 펄스
            }
            */

            // @@@@ [상태 셋팅]
            if (surfacing == Surfacing.Ground)
            {
                engineState = EngineState.Grounding;
                rigidbody.gravityScale = 0;
            }
            else
            {
                if (rigidbody.velocity.y > 0)
                {
                    engineState = EngineState.Ascending;
                }
                else if (rigidbody.velocity.y < 0)
                {
                    engineState = EngineState.Falling;
                }
                // 벨로시티가 0인데 땅위도 아니라면 벽에 붙었단거겠지.
            }

            // _IgnoreMovementUntil 변수로 구석에 있는상태에서 점프하면 수평이동 못하게 하는거 같음
            // @@@@ [좌우 이동]
            // 땅위인 경우
            if (surfacing == Surfacing.Ground)
            {
                rigidbody.drag = initialDrag; // ? 힘안가할때의 프레임당 감소치 였던가 ?
                rigidbody.AddForce(moveDir * groundAccel);
            }
            // 공중인 경우
            else
            {
                rigidbody.drag = 0;

                // 좌우가 비었거나 벽에 붙은상태인데 그 벽쪽으로 갈려는게 아닐때만 공중 이동
                if (((moveDir.x > 0) && (surfacing == Surfacing.LeftWall))
                    ||
                    ((moveDir.x < 0) && (surfacing == Surfacing.RightWall))
                    ||
                    surfacing == Surfacing.None)
                {
                    rigidbody.AddForce(moveDir * airAccel);
                }
            }


            //점프
            if (isJumpButtonPressed)
            {

            }
        }
    }
    #endregion

    // #### 디버그 ####
    private void OnDrawGizmos()
    {
        // 서페이싱 체커
        Bounds bound = GetComponent<BoxCollider2D>().bounds;
        Vector2 boundMax = bound.max;
        Vector2 boundMin = bound.min;
        // 아래
        boundMax.x -= surfacingCheckerDist;
        boundMin.x += surfacingCheckerDist;
        boundMax.y -= surfacingCheckerDist;
        boundMin.y -= surfacingCheckerDist;
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(new Vector2((boundMin.x + boundMax.x) / 2, (boundMin.y + boundMax.y) / 2), 
            new Vector2(boundMax.x - boundMin.x, boundMin.y - boundMax.y));
        boundMax = bound.max;
        boundMin = bound.min;
        // 오른쪽
        boundMax.x += surfacingCheckerDist;
        boundMin.x += surfacingCheckerDist;
        boundMax.y -= surfacingCheckerDist;
        boundMin.y += surfacingCheckerDist;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector2((boundMin.x + boundMax.x) / 2, (boundMin.y + boundMax.y) / 2), 
            new Vector2(boundMax.x - boundMin.x, boundMin.y - boundMax.y));
        boundMax = bound.max;
        boundMin = bound.min;
        //왼쪽 
        Gizmos.color = Color.blue;
        boundMax.x -= surfacingCheckerDist;
        boundMin.x -= surfacingCheckerDist;
        boundMax.y -= surfacingCheckerDist;
        boundMin.y += surfacingCheckerDist;
        Gizmos.DrawWireCube(new Vector2((boundMin.x + boundMax.x) / 2, (boundMin.y + boundMax.y) / 2),
            new Vector2(boundMax.x - boundMin.x, boundMin.y - boundMax.y));
    }
}
