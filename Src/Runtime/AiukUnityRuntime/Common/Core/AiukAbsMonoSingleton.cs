using UnityEngine;

namespace AiukUnityRuntime
{
    /// <summary>
    /// mono泛型单例。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AiukAbsMonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// mono单例对象挂载跟对象名。
        /// </summary>
        private const string RootName = "Aiuk";

        /// <summary>
        /// mono单例对象挂载跟对象。
        /// </summary>
        private static GameObject RootGo
        {
            get
            {
                var go = GameObject.Find(RootName) ?? new GameObject(RootName);
                return go;
            }
        }

        private static T m_Instance;

        /// <summary>
        /// 获得目标mono类型的单例对象。
        /// </summary>
        public static T Instance
        {
            get
            {
                if (m_Instance != null) return m_Instance;

                m_Instance = new GameObject(typeof(T).Name).AddComponent<T>();
                m_Instance.transform.SetParent(RootGo.transform);
                return m_Instance;
            }
        }

    }
}

