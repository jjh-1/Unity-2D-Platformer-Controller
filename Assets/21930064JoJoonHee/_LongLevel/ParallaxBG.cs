using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

// 레벨크기 제한되있으니 굳이 무한배경되게 만들필욘없을듯

public class ParallaxBG : MonoBehaviour
{
    public Vector2 bGMoveMultiplier; //멀리있는건 수치높게, 가까이있는건 낮게

    private Transform camCurrTransform; // 카메라 현위치 저장

    private Vector3 camLastPosition; // 이전프레임의 카메라 위치

    private void Start()
    {
        camCurrTransform = Camera.main.transform;
        camLastPosition = camCurrTransform.position;
    }

    // !!!! 카메라가 움직인후 수행해야하니 레이트 업데이트 (아니면 버벅거림)
    private void LateUpdate()
    {
        Vector3 deltaMove = camCurrTransform.position - camLastPosition;
        transform.position += new Vector3(deltaMove.x * bGMoveMultiplier.x, deltaMove.y * bGMoveMultiplier.y); // 카메라 속도보다 bGMoveMultiplier배 느리게 배경 움직임
        camLastPosition = camCurrTransform.position;
    }
}
