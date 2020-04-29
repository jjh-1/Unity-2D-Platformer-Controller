using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; // @ 오디오관련
using UnityEngine.UI;
using TMPro;
using TRANSGLOBAL; // 셋팅관련 변수 글로벌로 관리

public class SettingMenu : MonoBehaviour
{
    // 셋팅 글로벌로 저장되게함
    public Slider volumeSlider;
    public Toggle fullScreenToggle;
    public TMP_Dropdown qualityDropdown;

    private void Awake()
    {
        //볼륨 글로벌 셋팅
        volumeSlider.value = TransGlobal.volume;
        SetVolume(TRANSGLOBAL.TransGlobal.volume);
        //풀스크린 글로벌 셋팅
        fullScreenToggle.isOn = TRANSGLOBAL.TransGlobal.isFullScreen;
        SwitchFullScreen(TRANSGLOBAL.TransGlobal.isFullScreen);
        //퀄리티 글로벌 셋팅
        qualityDropdown.value = TRANSGLOBAL.TransGlobal.qualityIndex;
        SetQuality(TRANSGLOBAL.TransGlobal.qualityIndex);
    }
    
    // ! 이벤트에 연결해줄거니 메소드들 퍼블릭이어야함 !

    #region 오디오 믹서 컨트롤 관련 변수, 메소드
    // 인스펙터에서 레퍼런스 지정
    public AudioMixer audioMixer;
    // @!!!! 메소드의 파라미터가 float 이면 슬라이더의 유니티 이벤트 핸들러에서 다이나믹 플롯 옵션으로 이 메소드에 값주며 지정가능
    public void SetVolume(float newVolume)
    {
        // 볼륨셋팅 글로벌 저장
        TRANSGLOBAL.TransGlobal.volume = newVolume;

        // @ 첫번째 아규먼트는 유니티 믹서에서 옵션으로 노출시킨 볼륨 변수 커스텀 이름
        audioMixer.SetFloat("ExposedVolume", newVolume);
    }
    #endregion

    #region 퀄리티 변경 메소드
    // 마찬가지로 드랍박스 이벤트 핸들러의 옵션에 다이나믹 인트가 나옴
    public void SetQuality(int qualityIndex)
    {
        TRANSGLOBAL.TransGlobal.qualityIndex = qualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    #endregion

    #region 전체화면 메소드
    // 여억시 마찬가지로 토글 이벤트핸들러에서 자동으로 다이나믹 불 옵션 생김
    // ! 유니티 에디터에선 작동안하고 빌드 해야함 !
    public void SwitchFullScreen(bool isFullScreen)
    {
        TRANSGLOBAL.TransGlobal.isFullScreen = isFullScreen;
        Debug.Log("Switched FullScreen");
        Screen.fullScreen = isFullScreen;
    }
    #endregion

    #region 해상도 관련 변수, 메소드

    // 해상도 드랍다운 인스턴스 (인스펙터 지정)
    public TMP_Dropdown resolutionDropdown;

    // 리솔루션 정보 배열
    Resolution[] resolutions;

    // @@@@ 유저마다 모니터 다 다를테니 모니터에서 가능한 해상도 정보 가져와 지정  
    private void Start()
    {
        // 현재 모니터에 지원하는 모든 해상도 가져옴
        resolutions = Screen.resolutions;

        // 해상도 드랍다운 내용물들 클리어
        resolutionDropdown.ClearOptions();

        int currResolutionIndex = 0;

        // 스트럭트 배열인 resolutions 의 정보들을 스트링으로 변환후 리스트에 저장 후 드랍다운에 옵션추가
        List<string> resolutionOptions = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            // 대략 1920x1080 이런 포맷으로
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionOptions.Add(option);

            // 지원 가능한 해상도면 
            if ((resolutions[i].width == Screen.width)
                &&
                resolutions[i].height == Screen.height)
            {
                currResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    { 
        Debug.Log("new resolution set");
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);       
    }
    #endregion


}
