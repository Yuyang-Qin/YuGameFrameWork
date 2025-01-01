using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Script.Utility.DirtyFileRecorder
{
    [Serializable]
    public class RecorderData
    {
        public List<RecordData> records;
    }
    
    [Serializable]
    public class RecordData
    {
        public string fileName; 
        public long recordTimeStamp;
        public long version = 0;
    }
}