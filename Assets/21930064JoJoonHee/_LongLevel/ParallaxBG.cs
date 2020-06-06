using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBG : MonoBehaviour
{
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
        transform.position += deltaMove*0.5f; // 카메라 속도보다 0.5배 느리게 배경 움직임
        camLastPosition = camCurrTransform.position;
    }
}
