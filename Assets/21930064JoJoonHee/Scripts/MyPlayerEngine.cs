using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerEngine : MonoBehaviour
{

    // 플레이어 컨트롤에 의한 이동 방향은 건드려져션 안되니 이렇게 사용되는 곳에선 보호되게 프라이빗으로 받음
    private Vector2 moveDir;
    public void SetMoveDir(Vector2 moveDir)
    {
        this.moveDir = moveDir;
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
