using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // 인스펙터에 나오도록함
public class DialogueTrigger : MonoBehaviour
{
    // 이 두 변수는 따로 다른 클래스에 넣는게 성능에 더 좋긴할듯
    public string name;

    [TextArea(3, 10)] // 인스펙터 입력 포멧 변경
    public string[] dialogues;

    private bool isInTrigger = false;

    // @@@@ 온트리거/온콜라이더는 프레임마다 호출되는게 아니라 인풋 받으면 안됨 @@@@
    // 플레이어가 트리거 안에 있는동안
    private void OnTriggerStay2D(Collider2D collision) 
    {
        // 콜라이더 안에 있는게 플레이어면
        if (collision.name == "TestPlayer" )
        {
            isInTrigger = true;
        }
    }
    // 플레이어가 트리거안에서 나갈때
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "TestPlayer")
        {
            isInTrigger = false;
        }
    }

    private void Update()
    {
        if(isInTrigger)
        {
            // E키 눌렀을때
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("트리거안에있고e누르는중");
                // 다이얼로그 보여주는 중이 아니면
                if (FindObjectOfType<DialogueManager>().isShowingDialogue == false) // (FindObject보다 인스턴스 새로 만들고 인스펙터에서 지정하는게 성능에 더좋음)
                {
                    // 다이얼로그 시작
                    FindObjectOfType<DialogueManager>().StartDialogue(this);
                }
                // 보여주고 있는 중이면
                else
                {
                    // 다음 다이얼로그
                    FindObjectOfType<DialogueManager>().ShowNextDialogue();
                }
            }
        }
    }
}
