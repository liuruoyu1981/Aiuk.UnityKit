using UnityEngine;

namespace AiukUnityRuntime
{
    /// <summary>
    /// unity运行时上下文。 
    /// </summary>
    public class AiukUnityContext
    {
        /// <summary>
        /// 当前是否运行在桌面平台上，编辑器或PC下。
        /// </summary>
        public static bool IsDesktop
        {
            get
            {
                var platform = Application.platform;
                if (platform != RuntimePlatform.LinuxEditor
                    && platform != RuntimePlatform.OSXEditor
                    && platform != RuntimePlatform.WindowsEditor
                    && platform != RuntimePlatform.WindowsPlayer
                    && platform != RuntimePlatform.OSXPlayer)
                {
                    return false;
                }

                return true;
            }
        }
    }
}