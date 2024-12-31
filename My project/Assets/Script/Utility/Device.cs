using UnityEngine;
using Screen = UnityEngine.Device.Screen;

namespace Script.Utility
{
    public static class Device
    {
        public static string devicePlatform => Application.platform.ToString();
        public static string deviceModel => SystemInfo.deviceModel;
        public static string deviceType => SystemInfo.deviceType.ToString();
        
        public static float screenWidth => Screen.currentResolution.width;
        public static float screenHeight => Screen.currentResolution.height;

        public static int systemMemorySize => SystemInfo.systemMemorySize;
        public static int graphicsMemorySize => SystemInfo.graphicsMemorySize;

        public static string graphicsDevicName => SystemInfo.graphicsDeviceName;
        public static string graphicsDevicVender => SystemInfo.graphicsDeviceVendor;
        public static string graphicsDeviceType => SystemInfo.graphicsDeviceType.ToString();

        public static string unityVersion => Application.unityVersion;
    }
}