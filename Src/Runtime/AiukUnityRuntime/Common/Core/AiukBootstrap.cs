using UnityEngine;

namespace AiukUnityRuntime
{
    /// <summary>
    /// unity3d应用启动器。
    /// </summary>
    public class AiukBootstrap : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod]
        private static void StartUnityApp()
        {
            Debug.Log("Aiuk unity app started！");

            var unitySetting = AiukUnitySetting.GetDefaultSetting();
//            Debug.Log(unitySetting.AiukApps);

        }
    }
}
