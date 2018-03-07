using System;
using System.Collections.Generic;
using System.Linq;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 应用集合运行时设置。
    /// 该类用于提供所有App公用的基础设置项。
    /// 1. 服务实例名。
    /// 2. 第三方服务账号信息。
    /// 3. 各类服务器地址。
    /// </summary>
    [Serializable]
    public class AiukAppsRunSetting
    {
        #region 底层服务构建实例配置

        /// <summary>
        /// 构建目标服务类型映射字典。
        /// 用于保存应用启动时将要实际构建的服务类型。
        /// </summary>
        private readonly Dictionary<string, Type> m_ServiceTypes
        = new Dictionary<string, Type>();

        /// <summary>
        /// 添加一个将要被创建的服务类型。
        /// </summary>
        /// <returns>The service.</returns>
        /// <param name="type">Type.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public AiukAppsRunSetting SetService<T>(Type type)
            where T : IAiukUnityService
        {
            var serviceName = typeof(T).Name;
            if (m_ServiceTypes.ContainsKey(serviceName))
            {
                return this;
            }

            m_ServiceTypes.Add(serviceName, type);
            return this;
        }

        /// <summary>
        /// 获得一个服务的实现类型。
        /// </summary>
        /// <returns>The service type.</returns>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public Type GetServiceType<T>() where T : IAiukUnityService
        {
            var serviceName = typeof(T).Name;
            if (m_ServiceTypes.ContainsKey(serviceName))
            {
                var type = m_ServiceTypes[serviceName];
                return type;
            }

            return null;
        }

        #endregion

        #region 常用第三方配置项

        /// <summary>
        /// 微信的应用Id。
        /// </summary>
        public string WxBundleId { get; private set; }

        public AiukAppsRunSetting SetWxBundleId(string id)
        {
            WxBundleId = id;
            return this;
        }

        #endregion

        #region 常用配置

        /// <summary>
        /// 是否从AssetBundle中加载资源。
        /// 为真则按照StreamingAssets、沙盒、热更Http服务器的路径获取资源。
        /// 为假则使用反射加载应用模块AssetDatabase目录下的资源。
        /// </summary>
        /// <value><c>true</c> if load from asset bundle; otherwise, <c>false</c>.</value>
        public bool LoadFromAssetBundle { get; private set; }

        /// <summary>
        /// 开启AssetBundle加载。
        /// </summary>
        /// <returns>The asset bundle load.</returns>
        public AiukAppsRunSetting EnableAssetBundleLoad()
        {
            LoadFromAssetBundle = true;
            return this;
        }

        /// <summary>
        /// 热更新服务器地址，默认为本地地址。
        /// </summary>
        /// <value>The hot update http URL.</value>
        public string HotUpdateHttpUrl { get; private set; }

        /// <summary>
        /// 设置用于热更新的资源服务器地址。
        /// </summary>
        /// <returns>The hot http.</returns>
        /// <param name="url">URL.</param>
        public AiukAppsRunSetting SetHotHttp(string url)
        {
            HotUpdateHttpUrl = url;
            return this;
        }

        /// <summary>
        /// 心跳发送频率。
        /// </summary>
        /// <value>The heart frequency.</value>
        public int HeartFrequency { get; private set; }

        /// <summary>
        /// 设置心跳的发送频率。
        /// </summary>
        /// <returns>The heart frequency.</returns>
        /// <param name="frequency">Frequency.</param>
        public AiukAppsRunSetting SetHeartFrequency(int frequency)
        {
            HeartFrequency = frequency;
            return this;
        }

        /// <summary>
        /// 应用开发者列表。
        /// </summary>
        /// <value>The developers.</value>
        public List<string> Developers { get; private set; }

        public AiukAppsRunSetting SetDevelopers(params string[] developers)
        {
            Developers = developers.ToList();
            return this;
        }

        /// <summary>
        /// 开场视频名，默认为空。
        /// 不为空则自动播放。
        /// </summary>
        /// <value>The start video.</value>
        public string StartVideo { get; private set; }

        public AiukAppsRunSetting SetStartVideo(string name)
        {
            StartVideo = name;
            return this;
        }

        #endregion


        public AiukAppsRunSetting()
        {
            HotUpdateHttpUrl = "http://127.0.0.1/";
        }

    }
}


