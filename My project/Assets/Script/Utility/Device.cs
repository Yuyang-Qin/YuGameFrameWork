using System;
using UnityEngine;
using Screen = UnityEngine.Device.Screen;

namespace Script.Utility
{
    [Serializable]
    public class Device
    {
        private static Device _instance = null;
        public static Device instance = _instance ??= new Device();
        
        public string devicePlatform = Application.platform.ToString();
        public string deviceModel = SystemInfo.deviceModel;
        public string deviceType = SystemInfo.deviceType.ToString();
        
        public float screenWidth() => Screen.currentResolution.width;
        public float screenHeight() => Screen.currentResolution.height;

        public int systemMemorySize = SystemInfo.systemMemorySize;
        public int graphicsMemorySize = SystemInfo.graphicsMemorySize;

        public string graphicsDevicName = SystemInfo.graphicsDeviceName;
        public string graphicsDevicVender = SystemInfo.graphicsDeviceVendor;
        public string graphicsDeviceType = SystemInfo.graphicsDeviceType.ToString();

        public string unityVersion = Application.unityVersion;
    }
}