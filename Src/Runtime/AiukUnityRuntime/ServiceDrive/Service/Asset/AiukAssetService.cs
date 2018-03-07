using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aiuk.Common.Utility;
using AiukUnityRuntime.Utility;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 资源加载服务。
    /// </summary>
    public class AiukAssetService : AiukAbsUnityService
    {
        #region 字段

        private readonly Dictionary<string, Dictionary<string, AiukAssetInfo>>
        m_AssetInfos = new Dictionary<string, Dictionary<string, AiukAssetInfo>>();

        private readonly Dictionary<string, string> m_AssetTypeMap =
            new Dictionary<string, string>();

        private readonly Dictionary<string, AiukSpriteInfo> m_SpriteInfos
        = new Dictionary<string, AiukSpriteInfo>();

        private readonly Dictionary<string, string> m_SpriteTypeMap
        = new Dictionary<string, string>();

        private readonly Dictionary<string, AiukAppModuleSetting> m_ModuleMap
        = new Dictionary<string, AiukAppModuleSetting>();

        /// <summary>
        /// 基础AssetBundle缓存字典。
        /// 该字典中保存了应用中最常使用并且被其他资源依赖的AssetBundle包。
        /// </summary>
        private readonly Dictionary<string, AiukAssetBundleRef> m_CoreAssetBundleRefs
        = new Dictionary<string, AiukAssetBundleRef>();

        private readonly Dictionary<string, AiukAssetBundleRef> m_NormalAssetBundleRefs
        = new Dictionary<string, AiukAssetBundleRef>();

        #endregion

        #region 服务基础方法重写

        protected override void OnHotUpdateCompleted()
        {


        }

        #endregion

        #region 资源数据初始化

        private TextAsset LoadAssetInfoFromSandbox(string assetName, AiukAppModuleSetting module)
        {
            var sandboxPath = Application.persistentDataPath + string.Format(
                                  "/{0}/{1}/AssetBundle/Binary/{2}.assetbundle"
                                  , module.AppName, module.Token, assetName);

            var bundle = AssetBundle.LoadFromFile(sandboxPath);
            var textAsset = bundle.LoadAsset<TextAsset>(assetName);
            return textAsset;
        }

        private TextAsset LoadAssetInfoFromAssetDatabase
        (
            string assetName, AiukAppModuleSetting module
        )
        {

            return null;
        }

        private Dictionary<string, T> LoadAssetInfo<T>
        (
            string assetName,
            AiukAppModuleSetting module
        )
        {
            var textAsset = App.RunSetting.LoadFromAssetBundle ?
                                    LoadAssetInfoFromSandbox(assetName, module) :
                                    LoadAssetInfoFromAssetDatabase(assetName, module);

            if (textAsset == null)
            {
                throw new AiukAssetInfoLoadFailException(
                    string.Format("目标资源数据{0}加载失败", assetName));
            }

            var infos = AiukSerializeUtility.DeSerialize<Dictionary<string, T>>(textAsset.bytes);
            return infos;
        }

        private void InitAssetInfos()
        {
            var modules = App.AppModules;
            foreach (var item in modules)
            {

            }
        }

        private void InitSpriteInfos()
        {

        }



        #endregion

        #region 基础方法

        /// <summary>
        /// 获得目标资源的资源数据。
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public AiukAssetInfo GetAssetInfo(string assetName)
        {
            if (!m_AssetTypeMap.ContainsKey(assetName))
            {
                AiukDebugUtility.Log(string.Format("目标资源{0}没有资源数据！", assetName));
                return null;
            }

            var type = m_AssetTypeMap[assetName];
            var typeInfos = m_AssetInfos[type];
            var info = typeInfos[assetName];
            return info;
        }

        #endregion

        #region 加载

        #region 同步加载

        private AiukAssetBundleRef TryGetAssetBundleRefFromCache(AiukAssetInfo assetInfo)
        {
            if (!m_NormalAssetBundleRefs.ContainsKey(assetInfo.BundleName)) return null;

            var bundleRef = m_NormalAssetBundleRefs[assetInfo.BundleName];
            bundleRef.Use();
            return bundleRef;
        }

        private AiukAssetBundleRef SyncGetAssetBundle(AiukAssetInfo assetInfo)
        {
            var assetName = assetInfo.AssetName;
            var appModule = m_ModuleMap[assetInfo.LocModule];

            if (AiukAppModuleHelper.GetMainfest(appModule) == null)
            {
                AiukDebugUtility.LogError
                    ("Ab主描述文件为空，请确认执行了AssetBundle打包操作！");
                return null;
            }

            var manifest = AiukAppModuleHelper.GetMainfest(appModule);
            var depends = manifest.GetAllDependencies(assetName + ".assetbundle");

            foreach (var item in depends)
            {
                var bundleName = item.Replace(".assetbundle", "");
                if (m_CoreAssetBundleRefs.ContainsKey(bundleName))
                {
                    var bundleRef = m_CoreAssetBundleRefs[bundleName];
                    bundleRef.Use();
                }
                else
                {
                    var tempInfo = GetAssetInfo(bundleName);
                    var tempAb = AssetBundle.LoadFromFile(tempInfo.ImporterPath);
                    var bundleRef = new AiukAssetBundleRef(tempAb);
                    bundleRef.Use();
                    m_CoreAssetBundleRefs.Add(bundleName, bundleRef);
                    return bundleRef;
                }
            }

            var assetBundle = AssetBundle.LoadFromFile(assetInfo.ImporterPath);
            var newAbRef = new AiukAssetBundleRef(assetBundle);
            newAbRef.Use();
            m_NormalAssetBundleRefs.Add(assetInfo.BundleName, newAbRef);
            return newAbRef;
        }

        private void AsyncGetAssetBundle(AiukAssetInfo assetInfo, Action<AiukAssetBundleRef> callback)
        {
            var bundleRef = TryGetAssetBundleRefFromCache(assetInfo);
            if (bundleRef != null)
            {
                callback(bundleRef);
                return;
            }

            var assetName = assetInfo.AssetName;
            var appModule = m_ModuleMap[assetInfo.LocModule];

            if (AiukAppModuleHelper.GetMainfest(appModule) == null)
            {
                AiukDebugUtility.LogError
                    ("Ab主描述文件为空，请确认执行了AssetBundle打包操作！");
                return;
            }

            var manifest = AiukAppModuleHelper.GetMainfest(appModule);
            var depends = manifest.GetAllDependencies(assetName + ".assetbundle");
        }

        private List<T> AssetBundleSyncLoadAll<T>(AiukAssetInfo info)
            where T : Object
        {
            var bundle = SyncGetAssetBundle(info);
            var assets = bundle.AssetBundle.LoadAllAssets<T>().ToList();
            return assets;
        }

        #endregion

        #region 异步加载





        #endregion

        #endregion

        public Texture2D DynamicLoadTexture2D(string path, int width, int height)
        {
            var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            fs.Seek(0, SeekOrigin.Begin);
            var buffer = new byte[fs.Length];
            fs.Read(buffer, 0, (int)fs.Length);
            fs.Close();
            fs.Dispose();

            var texture2 = new Texture2D(width, height);
            texture2.LoadImage(buffer);
            return texture2;
        }


        public AiukViewRef SyncLoadView(string assetName)
        {
            return null;
        }

        public AiukMusicRef SyncLoadMusic(string assetName)
        {
            throw new NotImplementedException();
        }

        public AiukTextAssetRef SyncLoadTextAsset(string assetName)
        {
            throw new NotImplementedException();
        }

        public AiukTexture2DRef SyncLoadTexture2D(string assetName)
        {
            throw new NotImplementedException();
        }

        public AiukSoundRef SyncLoadSound(string assetName)
        {
            throw new NotImplementedException();
        }

        private List<T> AssetDatabaseSyncLoadAll<T>(AiukAssetInfo assetInfo)
            where T : Object
        {
            var assets = AiukAssetDatabaseUtility.LoadAllAssetsAtPath<T>(assetInfo.ImporterPath);
            return assets;
        }

        public List<T> SyncLoadAll<T>(string assetName) where T : Object
        {
            var assetInfo = GetAssetInfo(assetName);
            var runSetting = App.RunSetting;
            List<T> assets = null;

            if (runSetting.LoadFromAssetBundle)
            {

            }
            else
            {
                assets = AssetDatabaseSyncLoadAll<T>(assetInfo);
            }

            return assets;
        }

        public void SyncLoadView(string assetName, Action<AiukViewRef> callback)
        {
            throw new NotImplementedException();
        }

        public void SyncLoadMusic(string assetName, Action<AiukMusicRef> callback)
        {
            throw new NotImplementedException();
        }

        public void SyncLoadTextAsset(string assetName, Action<AiukTextAssetRef> callback)
        {
            throw new NotImplementedException();
        }

        public void SyncLoadTexture2D(string assetName, Action<AiukTexture2DRef> callback)
        {
            throw new NotImplementedException();
        }

        public void SyncLoadSound(string assetName, Action<AiukSoundRef> callback)
        {
            throw new NotImplementedException();
        }
    }
}

