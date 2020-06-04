using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator fadeAnimator; // 인스펙터에서 지정

    /*
    private void Update() 
    {
        if(Input.GetMouseButtonDown(0)) // 테스트
        {
            LoadNextLevel();
        }
    }
    */

    // 게임시작 버튼이나 특정 트리거 발동시 다음 씬 불러오게할 메소드
    public void LoadNextLevel()
    {
        StartCoroutine(ScreenFade(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator ScreenFade(int sceneBuildIndex)
    {
        // 페이드 애니메이션 시작
        fadeAnimator.SetTrigger("StartFade");

        // 해당 시간동안 대기
        // 고칠것 : 리터럴 대신 애니메이션에서 시간 가져오기
        yield return new WaitForSeconds(1f);

        // 다음 씬 불러옴
        SceneManager.LoadScene(sceneBuildIndex);
    }
}
