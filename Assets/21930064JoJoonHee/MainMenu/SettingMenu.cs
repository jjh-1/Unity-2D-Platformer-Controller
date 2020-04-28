using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; // @ 오디오관련 클래스들

public class SettingMenu : MonoBehaviour
{
    // ? 인스펙터에서 레퍼런스 지정 ?
    public AudioMixer audioMixer;

    // @!!!! 메소드의 파라미터가 float 이면 슬라이더의 유니티 이벤트 핸들러에서 다이나믹 플롯 옵션으로 이 메소드에 값주며 지정가능
    public void SetVolume (float volume)
    {
        // @ 첫번째 아규먼트는 유니티 믹서에서 옵션으로 노출시킨 볼륨 변수 커스텀 이름
        audioMixer.SetFloat("ExposedVolume", volume);
    }
}
