using System;
using System.Collections.Generic;
using System.IO;
using OdinSerializer;
using UnityEngine;

namespace Script.Utility.DirtyFileRecorder
{
    public static class DirtyFileRecorder
    {
        /// <summary>
        /// 更新脏文件记录
        /// </summary>
        /// <param name="folderPath">会检查该目录路径下所有文件，如果目录不存在就不会生成任何脏文件。</param>
        public static List<string> UpdateDirtyFileRecord(string folderPath)
        {
            var result = new List<string>();
                
            if (!Directory.Exists(folderPath))
            {
#if !UNITY_EDITOR
                Logger.Logger.LogError($"UpdateDirtyFileRecord :: {folderPath} does not exist");
#elif UNITY_EDITOR
                Debug.LogError($"UpdateDirtyFileRecord :: {folderPath} does not exist");
#endif
                return result;
            }

            var recordFilePath = Path.Combine(folderPath, "DirtyFileRecord.json");
            var recordFile = File.Open(recordFilePath, FileMode.OpenOrCreate);
            try
            {
                using (var reader = new StreamReader(recordFile))
                {
                    var content = reader.ReadToEnd();
                    var data = new RecorderData();
                    Dictionary<string, RecordData> cache = new();
                    if (content.Length > 0)
                    {
                        data = SerializationUtility.DeserializeValue<RecorderData>(
                            System.Text.Encoding.UTF8.GetBytes(content),
                            DataFormat.JSON);

                        foreach (var record in data.records)
                        {
                            cache[record.fileName] = record;
                        }
                    }

                    var files = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories);
                    foreach (var file in files)
                    {
                        var fileName = Path.GetFileName(file);
                        var recordData = cache.ContainsKey(fileName) ? cache[fileName] : new RecordData(){ fileName = fileName};
                        var fileLastWriteTime =
                            new DateTimeOffset(File.GetLastWriteTime(file)).ToUnixTimeMilliseconds();
                        if (recordData.recordTimeStamp < fileLastWriteTime || recordData.recordTimeStamp == default(long))
                        {
                            cache[fileName].version++;
                            cache[fileName].recordTimeStamp = fileLastWriteTime;
                            result.Add(file);
                        }
                    }
                }
            }
            catch (Exception e)
            {
#if !UNITY_EDITOR
                Logger.Logger.LogError($"UpdateDirtyFileRecord :: {folderPath} record error : {e}");
#elif UNITY_EDITOR
                Debug.LogError($"UpdateDirtyFileRecord :: {folderPath} record error : {e}");
#endif
                throw;
            }

            recordFile.Flush();
            recordFile.Close();

            return result;
        }
    }
}