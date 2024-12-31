using System;
using System.IO;
using System.Text;
using OdinSerializer;
using Unity.VisualScripting;
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
        private static StreamWriter _curStreamWriter = null;
        private static string _logFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "GameLog");
        private static string GetNewLogFilePath() => Path.Combine(_logFolderPath, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}.txt");
        
        public static void LogInfo(string message)
        {
            if (_curStreamWriter == null)
                Init();
            var content = FormatMessage(message, ELogLevel.Info);
#if UNITY_EDITOR
            Debug.Log(content);
#endif
            _curStreamWriter?.WriteLine(content);
        }

        public static void LogWarn(string message)
        {
            if (_curStreamWriter == null)
                Init();
            var content = FormatMessage(message, ELogLevel.Warn);
#if UNITY_EDITOR
            Debug.LogWarning(content);
#endif
            _curStreamWriter?.WriteLine(content);
        }

        public static void LogError(string message)
        {
            if (_curStreamWriter == null)
                Init();
            var content = FormatMessage(message, ELogLevel.Error);
#if UNITY_EDITOR
            Debug.LogError(content);
#endif
            _curStreamWriter?.WriteLine(content);
        }

        public static void Init()
        {
            CreateLogFile();
            PrintHeader();
        }

        public static void Finish()
        {
            if (_curStreamWriter != null)
            {
                _curStreamWriter.Close();
                _curStreamWriter = null;
            }
        }

        private static string FormatMessage(string message, ELogLevel level)
        {
            var curSB = new StringBuilder();
            curSB.Append(string.Concat('[', DateTime.Now, ']'))
                .Append(string.Concat('[', level.ToString(), ']'))
                .Append(string.Concat(':', message, '\n'));
            return curSB.ToString();
        }

        private static void CreateLogFile()
        {
            if (!Directory.Exists(_logFolderPath))
            {
                Directory.CreateDirectory(_logFolderPath);
            }
            
            try
            {
                _curStreamWriter = File.CreateText(GetNewLogFilePath());
                _curStreamWriter.AutoFlush = true;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private static string SerializeDeviceInfo()
        {
            var bytes = SerializationUtility.SerializeValue(Device.instance, DataFormat.JSON);
            var content = System.Text.Encoding.UTF8.GetString(bytes);
            return content;
        }

        private static void PrintHeader()
        {
            if (_curStreamWriter != null)
            {
                _curStreamWriter.WriteLine("*****************************   Time   *****************************");
                _curStreamWriter.WriteLine(DateTime.Now);
                _curStreamWriter.WriteLine("*****************************Device Info*****************************");
                _curStreamWriter.WriteLine(SerializeDeviceInfo());
                _curStreamWriter.WriteLine("*********************************************************************");
            }
        }
    }
}