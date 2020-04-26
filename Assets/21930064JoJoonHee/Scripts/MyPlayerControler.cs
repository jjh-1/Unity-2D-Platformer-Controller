using MYGLOBAL;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MyPlayerEngine))] // 오브젝트가 반드시 해당 컴포넌트 가지고있게함
public class MyPlayerControler : MonoBehaviour
{
    private MyPlayerEngine engine;
    void Start()
    {
        engine = GetComponent<MyPlayerEngine>(); // 엔진과 상호작용 위해 유니티 C# 스크립트 인스턴스 가져옴
    }

    // 키 입력은 프레임마다 정확할 필요 없으니 퍼포먼스 위해 컨트롤러 스크립트에서 그냥 업데이트에서 처리
    void Update()
    {
        // X축 인풋 셋
        engine.inputX = Input.GetAxis(MYGLOBAL.MyInput.BUTTON_HORIZONTAL_MOVE);

        #region ~~~~
        /*
        // [버튼에 의한 이동방향 지정]
        // 이동방향 변수. 누른 버튼이 오른쪽 화살표면 벡터 스트럭트 변수의 x값은 1, 왼쪽 화살표면 -1, 안움직이면 0
        Vector2 moveDir = new Vector2();
        moveDir.x = Input.GetAxis(MYGLOBAL.MyInput.BUTTON_HORIZONTAL_MOVE);
        // ! 나중 대비용 moveDir 벡터 y값 지정 코드
        //moveDir.y = Input.GetAxis(MYGLOBAL.MyInput.BUTTON_VERTICAL_MOVE);
        // 이동방향은 플레이어 컨트롤의 영역이니 여기서 처리하고 엔진 스크립트에 넘겨줌
        engine.SetMoveDir(moveDir);

        // [버튼누른 순간의 도약 점프]
        if(Input.GetButtonDown(MYGLOBAL.MyInput.BUTTON_JUMP))
        {
            engine.Jump();
        }

        // @변경@ [버튼 떼면 상승 멈추게 할것]
        if (Input.GetButtonUp(MYGLOBAL.MyInput.BUTTON_JUMP))
        {
            engine.JumpStop();
        }

        // [버튼 누르면 대쉬]
        if(Input.GetButtonDown(MYGLOBAL.MyInput.BUTTON_DASH))
        {
            engine.Dash();
        }
        */
        #endregion
    }
}
