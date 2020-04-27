using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MYGLOBAL
{
    public class MyInput
    {
        // 지정버튼 바꿔도 코드 바꿀 필요 없도록 인풋매니져에 지정해둔 이름 지정
        public const string BUTTON_HORIZONTAL_MOVE = "Horizontal";
        public const string BUTTON_VERTICAL_MOVE = "Vertical";
        public const string BUTTON_JUMP = "Jump";
        public const string BUTTON_DASH = "Fire1";
    }

    /*
    public class MyGlobals
    {
        public const string PACKAGE_NAME = "MYGLOBAL";

        public static int GetFrameCount(float time)
        {
            float frames = time / Time.fixedDeltaTime;
            int roundedFrames = Mathf.RoundToInt(frames);

            if (Mathf.Approximately(frames, roundedFrames))
            {
                return roundedFrames;
            }

            return Mathf.CeilToInt(frames);
        }
    }
    */
}



