using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private LevelManager levelManager;
    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //이 체크포인트의 트리거안에 들가면 레벨매니져의 체크포인트를 이거로
        if(collision.name =="TestPlayer") 
        {
            levelManager.currCheckPoint = gameObject;
        }
    }
}
