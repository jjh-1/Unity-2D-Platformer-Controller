using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject currCheckPoint;

    public Animator fadeAnimator;

    private PlayerController2D player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController2D>();
    }

    public void RespawnPlayer()
    {
        StartCoroutine(ScreenFadee());
    }

    IEnumerator ScreenFadee()
    {
        FindObjectOfType<DialogueManager>().isShowingDialogue = true;

        // 페이드 아웃 애니메이션 시작
        fadeAnimator.SetTrigger("StartFade");

        // 해당 시간동안 대기
        // 고칠것 : 리터럴 대신 애니메이션에서 시간 가져오기
        yield return new WaitForSeconds(1f); // 1초 대기

        // 페이드아웃 끝나면 체크포인트로 이동
        player.transform.position = currCheckPoint.transform.position;
        FindObjectOfType<DialogueManager>().isShowingDialogue = false;

        //페이드인
        fadeAnimator.SetTrigger("EndFade");

    }
}
