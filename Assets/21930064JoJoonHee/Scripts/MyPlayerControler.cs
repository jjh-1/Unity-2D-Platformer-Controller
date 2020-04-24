using MYGLOBAL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MyPlayerEngine))] // 반드시 해당 컴포넌트 연결하게
public class MyPlayerControler : MonoBehaviour
{
    public bool canControl = true; // 최상위 전체 컨트롤 제어 불리언

    private MyPlayerEngine engine;

    // Start is called before the first frame update
    void Start()
    {
        engine = GetComponent<MyPlayerEngine>(); // 유니티 C# 스크립트 인스턴스 가져옴
    }

    // Update is called once per frame
    void Update()
    {
        if (!canControl)
        {
            return; // 최상위 전체 컨트롤 제어 불리언
        }

        // [버튼에 의한 이동방향 지정]
        // 이동방향 변수. 누른 버튼이 오른쪽 화살표면 벡터 스트럭트 변수의 x값은 1, 왼쪽 화살표면 -1, 안움직이면 0
        Vector2 moveDir = new Vector2();
        moveDir.x = Input.GetAxis(MYGLOBAL.MyInput.BUTTON_HORIZONTAL_MOVE);
        // 이동방향은 플레이어 컨트롤의 영역이니 여기서 처리하고 엔진 스크립트에 넘겨줌
        engine.SetMoveDir(moveDir);

        // [버튼에 의한 점프]
        if(Input.GetButtonDown(MYGLOBAL.MyInput.BUTTON_JUMP)

    }
}
