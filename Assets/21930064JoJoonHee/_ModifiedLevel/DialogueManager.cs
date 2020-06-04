using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogueManager : MonoBehaviour
{
    // 인스펙터에서 지정
    public Animator animator;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    private Queue<string> dialogueQueue;

    public bool isShowingDialogue = false; // 다이얼로그 보여주는중 못움직이게용

    private void Start()
    {
        dialogueQueue = new Queue<string>();
    }

    public void StartDialogue (DialogueTrigger dT)
    {
        // 패널 펼치게 애니메이터 불리언 셋
        animator.SetBool("IsDialoguePanelOpen", true);

        isShowingDialogue = true;

        nameText.text = dT.name;
        // 큐에 전에 들어있던 문장들 삭제하고
        dialogueQueue.Clear();

        // 각각의 오브젝트에 지정된 다이얼로그 트리거의 
        // 다이얼로그 배열 내용들을 매니저 스크립트의 큐에 새로 넣음
        foreach (string s in dT.dialogues)
        {
            dialogueQueue.Enqueue(s);
        }

        ShowNextDialogue();
    }
    
    // e키같은거 눌렀을때 호출할것
    public void ShowNextDialogue()
    {
        if (dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        // 하나씩 디큐후 텍스트 오브젝트에 지정
        string dialogue = dialogueQueue.Dequeue();
        StopAllCoroutines(); // @ 혹시 다 끝나기전에 e키눌러 스킵해서 새 코루틴 시작하면 얽혀버리니 그거 방지  
        StartCoroutine(DialogueOneByOne(dialogue));
    }

    // 글자 하나씩 출력하기위한 이누머레이터
    IEnumerator DialogueOneByOne(string dialogue)
    {
        dialogueText.text = "";
        foreach (char l in dialogue.ToCharArray()) // ToCharArray() : 스트링을 char배열로
        {
            dialogueText.text += l; // 돌때마다 하나씩 추가
            yield return new WaitForSeconds(0.025f); // WaitForSeconds 메소드의 아규먼트초마다 글자 하나씩 나오게. 메소드 말고 그냥 yeild return null 은 1프레임 대기
        }
    }

    private void EndDialogue()
    {
        // 패널 닫게 애니메이터 불리언 셋
        animator.SetBool("IsDialoguePanelOpen", false);
        isShowingDialogue = false;
    }
}
