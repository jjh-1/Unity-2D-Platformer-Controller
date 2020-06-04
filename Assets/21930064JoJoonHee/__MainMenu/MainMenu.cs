using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리 할려면 필요한 유니티 빌트인 네임 스페이스

public class MainMenu : MonoBehaviour
{
    // 22인스펙터에서 지정 방식이 성능엔 더좋긴함
    // public SceneLoader sceneLoader;
    public void PlayGame()
    {
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
}
