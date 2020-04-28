using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; // @ 오디오관련

public class SettingMenu : MonoBehaviour
{
    // ! 이벤트에 연결해줄거니 메소드들 퍼블릭이어야함 !

    #region 오디오 믹서 컨트롤 관련 변수, 메소드
    // ? 인스펙터에서 레퍼런스 지정 ?
    public AudioMixer audioMixer;
    // @!!!! 메소드의 파라미터가 float 이면 슬라이더의 유니티 이벤트 핸들러에서 다이나믹 플롯 옵션으로 이 메소드에 값주며 지정가능
    public void SetVolume (float volume)
    {
        // @ 첫번째 아규먼트는 유니티 믹서에서 옵션으로 노출시킨 볼륨 변수 커스텀 이름
        audioMixer.SetFloat("ExposedVolume", volume);
    }
    #endregion

    #region 퀄리티 변경 메소드
    // 마찬가지로 드랍박스 이벤트 핸들러의 옵션에 다이나믹 인트가 나옴
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    #endregion
}
