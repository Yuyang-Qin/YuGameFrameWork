using System;
using System.Text;
using UnityEngine;

namespace Script.Utility.Logger
{
    public enum ELogLevel
    {
        Info,
        Warn,
        Error
    }
    
    public class Logger
    {
        public static void LogInfo(string message)
        {
            var content = FormatMessage(message, ELogLevel.Info);
#if UNITY_EDITOR
            Debug.Log(content);
#endif
        }

        public static void LogWarn(string message)
        {
            var content = FormatMessage(message, ELogLevel.Warn);
#if UNITY_EDITOR
            Debug.LogWarning(content);
#endif
        }

        public static void LogError(string message)
        {
            var content = FormatMessage(message, ELogLevel.Error);
#if UNITY_EDITOR
            Debug.LogError(content);
#endif
        }

        private static string FormatMessage(string message, ELogLevel level)
        {
            var curSB = new StringBuilder();
            curSB.Append(string.Concat('[', DateTime.Now ,']'))
                .Append(string.Concat('[', level.ToString() ,']'))
                .Append(string.Concat(':', message, '\n'));
            return curSB.ToString();
        }
        
        
    }
}