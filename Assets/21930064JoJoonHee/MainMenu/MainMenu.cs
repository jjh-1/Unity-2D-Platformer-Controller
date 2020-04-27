using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // 일단 빌드세팅에 넣어둔 씬들 인덱스로 씬전환
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    { 
        Debug.Log("Quit Button Pressed");
        // 유니티 에디터에선 작동 안하고 빌드해서 독립 프로그램 됬을때 작동
        Application.Quit();
    }
}
