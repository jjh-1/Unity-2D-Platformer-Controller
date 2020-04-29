using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TRANSGLOBAL // 셋팅관련 변수 글로벌로 관리
{
    public class TransGlobal : MonoBehaviour
    {
        // 셋팅관련 변수 글로벌로 관리
        public static float volume = 0;


        // 오디오 관련
        private static TransGlobal audioManager;
        private void Awake()
        {
            // 씬 전환시 해당 오브젝트는 그대로 남음
            DontDestroyOnLoad(this);

            // 다시 메인화면씬 로드시 오디오메니저 다시 생기는거 방지
            if (audioManager == null)
            {
                audioManager = this;
            }
            else
            {
                DestroyObject(gameObject);
            }
        }
    }
}

