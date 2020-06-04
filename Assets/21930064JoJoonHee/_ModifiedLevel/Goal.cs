using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public SceneLoader sceneLoader;

    // 리지드바디 컴포넌트 있어야 작동함
    // 골의 콜라이더에 플레이어가 들어가면 다음 씬 불러옴
    void OnTriggerEnter2D(Collider2D o)
    {
        if(o.name == "TestPlayer")
        {
            sceneLoader.LoadNextLevel();
        }
        
    }

}
