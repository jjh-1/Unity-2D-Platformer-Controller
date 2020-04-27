using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPauseMenu : MonoBehaviour
{
    // 액티브 <-> 디액티브 할것
    public GameObject pauseMenu;

    // 복) 스태틱 붙인 변수는 인스턴스마다 따로 있는게 아니라 단 하나
    public static bool isGamePaused = false;

    // 일시정지 메소드
    private void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0; // 0 이면 시간 멈춤
        isGamePaused = true;
    }

    // 게임 재개 메소드
    // 리슘 버튼에 이벤트 연결 해줘야하니 퍼블릭
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1; // 1이면 원래 시간 스케일
        isGamePaused = false;
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
