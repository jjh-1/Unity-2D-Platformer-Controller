using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리 할려면 필요한 유니티 빌트인 네임 스페이스
using TRANSGLOBAL;
using TMPro;
public class MainMenu : MonoBehaviour
{
    #region 중간때 코드들(메인화면이벤트)
    // 22인스펙터에서 지정 방식이 성능엔 더좋긴함
    // public SceneLoader sceneLoader;
    public void PlayGame()
    {
        // 트루면 플레이어 위치를 로디드포지션으로 하게할 플래그
        TRANSGLOBAL.TransGlobal.isLoadedGame = false;

        // 22인스펙터에서 지정 방식이 성능엔 더좋긴함
        FindObjectOfType<SceneLoader>().LoadNextLevel();
        // 일단 빌드세팅에 넣어둔 씬들 인덱스로 씬전환
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    { 
        Debug.Log("Quit Button Pressed");
        // 유니티 에디터에선 작동 안하고 빌드해서 독립 프로그램 됬을때 작동
        Application.Quit();
    }
    #endregion

    // !인스펙터지정
    public TextMeshProUGUI loadText;

    //!로드버튼에 이벤트연결
    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("playerPosX")) // 세이브 한적 있으면
        {
            TRANSGLOBAL.TransGlobal.isLoadedGame = true; // 트루면 플레이어 위치를 로디드포지션으로 하게할 플래그
            TRANSGLOBAL.TransGlobal.loadedPlayerPos.x = PlayerPrefs.GetFloat("playerPosX");
            TRANSGLOBAL.TransGlobal.loadedPlayerPos.y = PlayerPrefs.GetFloat("playerPosY");

            FindObjectOfType<SceneLoader>().LoadSavedLevel(PlayerPrefs.GetInt("sceneInd"));
        }
        else
        {
            // 세이브했던 키 없으면 세이브파일없다고 로드버튼의 텍스트 변경
            loadText.SetText("THERE IS NO SAVE FILE!");
        }
    }

    private void Awake()
    {
        // 다시 메인메뉴씬 불러왔을때 로드텍스트 초기화
        loadText.SetText("LOAD");
    }
}
