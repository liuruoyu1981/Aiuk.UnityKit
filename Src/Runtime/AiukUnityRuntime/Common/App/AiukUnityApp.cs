using System;
using System.Collections.Generic;
using Aiuk.Common.Utility;

namespace AiukUnityRuntime
{
    /// <summary>
    /// unity应用实体。
    /// </summary>
    public class AiukUnityApp : IAiukUnityApp
    {
        #region 服务

        /// <summary>
        /// 服务实例字典
        /// </summary>
        protected readonly Dictionary<string, IAiukUnityService> Services
        = new Dictionary<string, IAiukUnityService>();

        /// <summary>
        /// 服务类型字典
        /// </summary>
        protected Dictionary<string, Type> ServiceTypes = new Dictionary<string, Type>();

        /// <summary>
        /// 获得指定的服务实例，如果实例不存在将抛出异常。
        /// </summary>
        /// <typeparam name="T">泛型服务类型。</typeparam>
        /// <returns></returns>
        public T GetService<T>() where T : class, IAiukUnityService
        {
            var typeName = typeof(T).Name;
            if (!ServiceTypes.ContainsKey(typeName))
            {
                throw new Exception(string.Format("指定的{0}服务不存在！", typeName));
            }

            var service = Services[typeName] as T;
            return service;
        }

        #endregion

        #region 设置注入

        public AiukAppsRunSetting RunSetting { get; private set; }

        public AiukUnityApp SetRunSetting(AiukAppsRunSetting setting)
        {
            RunSetting = setting;
            return this;
        }

        /// <summary>
        /// 实际运行的模块列表。
        /// </summary>
        /// <value>The app modules.</value>
        public List<AiukAppModuleSetting> AppModules { get; private set; }

        /// <summary>
        /// 添加一个将要启动的应用模块。
        /// </summary>
        /// <returns>The app module.</returns>
        /// <param name="module">Module.</param>
        public AiukUnityApp SetAppModule(AiukAppModuleSetting module)
        {
            var exist = AppModules.Find(m => m.Token == module.Token);
            if (exist != null)
            {
                AiukDebugUtility.LogError(
                    string.Format("目标模块设置{0}当前已存在！", module.Token));
                return this;
            }

            AppModules.Add(module);
            return this;
        }


        #endregion

        public virtual void Dispose()
        {

        }

        public AiukUnityApp()
        {
            AppModules = new List<AiukAppModuleSetting>();
        }
    }
}

