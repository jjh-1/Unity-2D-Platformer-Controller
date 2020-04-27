using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리 할려면 필요한 유니티 빌트인 네임 스페이스

// !!!! 게임 시작시 비활성화된 오브젝트에 지정된 스크립트는 실행안되니 정지메뉴 오브젝트가 아니라 캔버스에 스크립트 놔야함
public class ControlPauseMenu : MonoBehaviour
{
    // 액티브 <-> 디액티브 할것
    public GameObject pauseMenu;

    // 복) 스태틱 붙인 변수는 인스턴스마다 따로 있는게 아니라 단 하나
    public static bool isGamePaused = false;

    // 메인메뉴로 돌아가는 메소드
    public void GotoMainMenuScene()
    {
        Time.timeScale = 1; // ! 일시정지 메소드에서 0으로 만들었으니 다시 정상으로
        SceneManager.LoadScene("Test Main Menu"); // 하드코딩
    }

    // 게임종료 메소드
    public void QuitGame()
    {
        Debug.Log("Quit Button Pressed");
        // 유니티 에디터에선 작동 안하고 빌드해서 독립 프로그램 됬을때 작동
        Application.Quit();
    }

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
            }
        }
    }
}
