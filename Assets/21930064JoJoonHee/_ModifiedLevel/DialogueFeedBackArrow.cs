using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 다이얼로그트리거랑 통합?
public class DialogueFeedBackArrow : MonoBehaviour
{
    public float offset = 1.5f;
    public float floatingSpd = 12.5f; // 둥둥 속도
    public float floatingAmount = 0.2f; //둥둥 간격

    void Update()
    {
        //위아래로 둥둥 떠다니는 효과
        float y = transform.parent.position.y + offset + (Mathf.Sin(Time.time* floatingSpd) * floatingAmount);
        transform.position = new Vector3(transform.parent.position.x, y, transform.parent.position.z);
    }
}
