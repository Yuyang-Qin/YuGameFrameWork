using System;
using Script.Utility;
using UnityEngine;
using Script.Utility.Logger;
using customLogger = Script.Utility.Logger.Logger;

namespace Script
{
    public class GameEntry : MonoBehaviour
    {
        private void Awake()
        {
            customLogger.Init();
        }

        private void OnDestroy()
        {
            customLogger.Finish();
        }
    }
}