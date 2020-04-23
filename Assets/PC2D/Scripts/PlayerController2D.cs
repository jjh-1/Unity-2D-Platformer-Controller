using UnityEngine;

/// <summary>
/// This class is a simple example of how to build a controller that interacts with PlatformerMotor2D.
/// </summary>
[RequireComponent(typeof(PlatformerMotor2D))] // 복) 인스펙터에서 반드시 자식 지정 하도록함
public class PlayerController2D : MonoBehaviour
{
    private PlatformerMotor2D _motor; // 변수 조정용 클래스
    private bool _restored = true; // ?원웨이 플랫폼 관련 변수?
    private bool _enableOneWayPlatforms;
    private bool _oneWayPlatformsAreWalls;

    // Use this for initialization
    void Start()
    {
        _motor = GetComponent<PlatformerMotor2D>(); // 복) 인스턴스 지정
    }

    // before enter en freedom state for ladders
    void FreedomStateSave(PlatformerMotor2D motor)
    {
        if (!_restored) // do not enter twice
            return;

        _restored = false;
        _enableOneWayPlatforms = _motor.enableOneWayPlatforms;
        _oneWayPlatformsAreWalls = _motor.oneWayPlatformsAreWalls;
    }
    // after leave freedom state for ladders
    void FreedomStateRestore(PlatformerMotor2D motor)
    {
        if (_restored) // do not enter twice
            return;

        _restored = true;
        _motor.enableOneWayPlatforms = _enableOneWayPlatforms;
        _motor.oneWayPlatformsAreWalls = _oneWayPlatformsAreWalls;
    }

    // Update is called once per frame
    void Update()
    {
        // use last state to restore some ladder specific values
        if (_motor.motorState != PlatformerMotor2D.MotorState.FreedomState)
        {
            // try to restore, sometimes states are a bit messy because change too much in one frame
            FreedomStateRestore(_motor);
        }

        // Jump?
        // If you want to jump in ladders, leave it here, otherwise move it down
        if (Input.GetButtonDown(PC2D.Input.JUMP))
        {
            _motor.Jump();
            _motor.DisableRestrictedArea();
        }

        _motor.jumpingHeld = Input.GetButton(PC2D.Input.JUMP);

        // XY freedom movement
        if (_motor.motorState == PlatformerMotor2D.MotorState.FreedomState)
        {
            _motor.normalizedXMovement = Input.GetAxis(PC2D.Input.HORIZONTAL);
            _motor.normalizedYMovement = Input.GetAxis(PC2D.Input.VERTICAL);

            return; // do nothing more
        }

        // X axis movement
        if (Mathf.Abs(Input.GetAxis(PC2D.Input.HORIZONTAL)) > PC2D.Globals.INPUT_THRESHOLD)
        {
            _motor.normalizedXMovement = Input.GetAxis(PC2D.Input.HORIZONTAL);
        }
        else
        {
            _motor.normalizedXMovement = 0;
        }

        // 위나 아래 버튼 눌렀을때
        if (Input.GetAxis(PC2D.Input.VERTICAL) != 0)
        {
            bool up_pressed = Input.GetAxis(PC2D.Input.VERTICAL) > 0; // 위버튼이었으면 t 아니면 f 플래그
            // @@@@ 기능추가 원웨이 플랫폼 위에서 아래 버튼이랑 점프 눌렀을때 아래로 가기 @@@@
            if (_motor.IsOnLadder()) // 사다리 타는중 상태일때 
            {
                if (
                    (up_pressed && _motor.ladderZone == PlatformerMotor2D.LadderZone.Top) // 위버튼을 눌렀고, 사다리 제일 위라면
                    ||
                    (!up_pressed && _motor.ladderZone == PlatformerMotor2D.LadderZone.Bottom) // 아래버튼을 눌렀고, 사다리 제일 아래라면
                 )
                {
                    // do nothing!
                }
                // if player hit up, while on the top do not enter in freeMode or a nasty short jump occurs
                else
                {
                    // example ladder behaviour

                    _motor.FreedomStateEnter(); // enter freedomState to disable gravity
                    _motor.EnableRestrictedArea();  // movements is retricted to a specific sprite bounds

                    // now disable OWP completely in a "trasactional way"
                    FreedomStateSave(_motor);
                    _motor.enableOneWayPlatforms = false;
                    _motor.oneWayPlatformsAreWalls = false;

                    // start XY movement
                    _motor.normalizedXMovement = Input.GetAxis(PC2D.Input.HORIZONTAL);
                    _motor.normalizedYMovement = Input.GetAxis(PC2D.Input.VERTICAL);
                }
            }
        }
        else if (Input.GetAxis(PC2D.Input.VERTICAL) < -PC2D.Globals.FAST_FALL_THRESHOLD)
        {
            _motor.fallFast = false;
        }

        if (Input.GetButtonDown(PC2D.Input.DASH))
        {
            _motor.Dash();
        }
    }
}
