using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager audioManager;

    private void Awake()
    {
        // 씬 전환시 해당 오브젝트는 그대로 남음
        DontDestroyOnLoad(this);

        // 다시 메인화면씬 로드시 오디오메니저 다시 생기는거 방지
        if(audioManager == null)
        {
            audioManager = this;
        }
        else
        {
            DestroyObject(gameObject);
        }
    }
}
