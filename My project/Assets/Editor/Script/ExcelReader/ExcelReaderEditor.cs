using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor.Script.ExcelReader
{
    public class ExcelReaderEditor
    {
        private static string _excelFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "excels");
        [MenuItem("Tools/ReadExcel")]
        public static void ReadExcel()
        {
            Debug.Log("Starting Read Excel!");
            CheckFolder(_excelFolderPath);
            
        }

        private static void CheckFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                Debug.LogWarning($"The target path does not exist, {folderPath} : Completing");
                Directory.CreateDirectory(folderPath);
            }
        }
    }
}