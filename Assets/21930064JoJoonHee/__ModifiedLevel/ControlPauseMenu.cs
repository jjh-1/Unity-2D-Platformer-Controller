using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리 할려면 필요한 유니티 빌트인 네임 스페이스
using TMPro;

// !!!! 게임 시작시 비활성화된 오브젝트에 지정된 스크립트는 실행안되니 정지메뉴 오브젝트가 아니라 캔버스에 스크립트 놔야함
public class ControlPauseMenu : MonoBehaviour
{
    #region 중간때 코드들 (정지화면 이벤트)
    // 액티브 <-> 디액티브 할 오브젝트
    public GameObject pauseMenu;

    // 복) 스태틱 붙인 변수는 인스턴스마다 따로 있는게 아니라 단 하나
    public static bool isGamePaused = false;

    // 메인메뉴 씬으로 가는 메소드
    public void GotoMainMenuScene()
    {
        Time.timeScale = 1; // ! 일시정지 메소드에서 0으로 만들었으니 다시 정상으로 해줘야함
        SceneManager.LoadScene("Test Main Menu"); // 하드코딩
    }

    // 게임종료 메소드
    public void QuitGame()
    {
        Debug.Log("Quit Button Pressed");
        // 유니티 에디터에선 작동 안하고 빌드해서 독립 프로그램 됬을때 작동
        Application.Quit();
    }
    // TODO : 글로벌 메소드로

    // 게임 재개 메소드
    // 리슘 버튼에 이벤트 연결 해줘야하니 퍼블릭
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1; // 1이면 원래 시간 스케일
        isGamePaused = false;
    }
    // 일시정지 메소드
    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0; // 0 이면 시간 멈춤
        isGamePaused = true;
    }
    #endregion

    // !인스펙터지정
    public TextMeshProUGUI saveText;

    //!세이브버튼에 이벤트 연결
    public void SaveGame()
    {
        //플레이어의 위치 가져옴
        Vector3 playerPos = FindObjectOfType<PlayerController2D>().transform.position;
        //PlayerPrefs : 유니티기본 외부파일저장방식. 윈도우의경우 레지스터에저장
        PlayerPrefs.SetFloat("playerPosX", playerPos.x);
        PlayerPrefs.SetFloat("playerPosY", playerPos.y);
        PlayerPrefs.SetInt("sceneInd", SceneManager.GetActiveScene().buildIndex); //현재 씬의 빌드 인덱스 저장
        PlayerPrefs.Save();

        //세이브됬다 피드백
        saveText.SetText("SAVED!");
    }

    // 모노비헤비어 메소드
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
                // 다시 정지했을때 세이브텍스트 초기화
                saveText.SetText("SAVE");
            }
        }
    }
}
