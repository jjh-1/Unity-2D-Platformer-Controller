using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogueManager : MonoBehaviour
{
    // 인스펙터에서 지정
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

        string dialogue = dialogueQueue.Dequeue();
        dialogueText.text = dialogue;
    }

    private void EndDialogue()
    {
        isShowingDialogue = false;
    }
}
