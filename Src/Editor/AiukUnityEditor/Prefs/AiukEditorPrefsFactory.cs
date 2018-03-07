using System;

namespace Aiuk.Unity.Common
{
    /// <summary>
    /// 编辑器持久化数据工厂。
    /// </summary>
    public static class AiukEditorPrefsFactory
    {
        /// <summary>
        /// 获得一个目标类型的编辑器持久数据实例。
        /// </summary>
        /// <typeparam name="TClass">目标数据类型。</typeparam>
        /// <typeparam name="TData">目标基础数据类型。</typeparam>
        /// <param name="key">目标数据的Key。</param>
        /// <param name="defaultValue">目标数据的默认值，该值会在初始化时进行保存会可随时访问。</param>
        /// <returns></returns>
        public static IAiukPrefs<TData> GetPrefs<TClass, TData>(string key, TData defaultValue)
            where TClass : IAiukPrefs<TData>
        {
            var prefs = Activator.CreateInstance<TClass>();
            prefs.InitPrefs(key, defaultValue);
            prefs.SetValue(defaultValue);

            return prefs;
        }
    }
}
