using System.Collections.Generic;
using UnityEngine;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 应用模块助手。
    /// 获取指定应用模块的各种常用路径字符串。
    /// 获取指定应用模块的各种资源引用。
    /// </summary>
    public class AiukAppModuleHelper
    {
        private readonly AiukAppModuleSetting m_AppModule;

        public AiukAppModuleHelper(AiukAppModuleSetting appModule)
        {
            m_AppModule = appModule;
        }

        #region 路径

        #region Cs

        public string CsRootDir
        {
            get
            {
                var path = m_AppModule.RootDir + "Cs/";
                return path;
            }
        }

        public string CsNetRequestDir
        {
            get
            {
                var path = CsRootDir + "NetRequest/";
                return path;
            }
        }

        public string CsNetResponseDir
        {
            get
            {
                var path = CsRootDir + "NetResponse/";
                return path;
            }
        }

        public string CsDllDir
        {
            get
            {
                var path = CsRootDir + "Dll/";
                return path;
            }
        }

        public string CsEditorDir
        {
            get
            {
                var path = CsRootDir + "Editor/";
                return path;
            }
        }

        /// <summary>
        /// 开发工具包接口重写脚本保存目录。
        /// </summary>
        /// <value>The cs aiuk unity kit override.</value>
        public string CsAiukUnityKitOverrideDir
        {
            get
            {
                var path = CsRootDir + "AiukUnityKitOverride/";
                return path;
            }
        }

        public string CsLocalDataDir
        {
            get
            {
                var path = CsRootDir + "LocalData/";
                return path;
            }
        }

        public string CsMonoEntitesDir
        {
            get
            {
                var path = CsRootDir + "MonoEntities/";
                return path;
            }
        }

        public string CsMvdaDir
        {
            get
            {
                var path = CsRootDir + "MVDA/";
                return path;
            }
        }

        public string CsReactiveDataDir
        {
            get
            {
                var path = CsRootDir + "ReactiveData/";
                return path;
            }
        }

        #endregion

        #region AssetDatabase

        public string AssetDatabaseRootDir
        {
            get
            {
                var path = m_AppModule.RootDir + "AssetDatabase/";
                return path;
            }
        }

        public string AssetDatabaseMusicDir
        {
            get
            {
                var path = AssetDatabaseRootDir + "Music/";
                return path;
            }
        }

        public string AssetDatabaseAtlasDir
        {
            get
            {
                var path = AssetDatabaseRootDir + "Atlas/";
                return path;
            }
        }

        public string AssetDatabaseBinaryDir
        {
            get
            {
                var path = AssetDatabaseRootDir + "Binary/";
                return path;
            }
        }

        public string AssetDatabaseFontDir
        {
            get
            {
                var path = AssetDatabaseRootDir + "Font/";
                return path;
            }
        }

        public string AssetDatabaseLocalDataDir
        {
            get
            {
                var path = AssetDatabaseRootDir + "LocalData/";
                return path;
            }
        }

        public string AssetDatabaseMaterial
        {
            get
            {
                var path = AssetDatabaseRootDir + "Material/";
                return path;
            }
        }

        public string AssetDatabaseShader
        {
            get
            {
                var path = AssetDatabaseRootDir + "Shader/";
                return path;
            }
        }

        public string AssetDatabaseView
        {
            get
            {
                var path = AssetDatabaseRootDir + "View/";
                return path;
            }
        }

        public string AssetDatabaseSoundEffect
        {
            get
            {
                var path = AssetDatabaseRootDir + "SoundEffect/";
                return path;
            }
        }

        public string AssetDatabaseTexture
        {
            get
            {
                var path = AssetDatabaseRootDir + "Texture/";
                return path;
            }
        }


        #endregion

        #region OriginalAsset

        public string OriginalRootDir
        {
            get
            {
                var path = m_AppModule.RootDir + "OriginalAsset/";
                return path;
            }
        }

        public string AtlasSpriteDir
        {
            get
            {
                var path = OriginalRootDir + "AtlasSprite/";
                return path;
            }
        }

        public string AtlasSplitDir
        {
            get
            {
                var path = OriginalRootDir + "AtlasSplit/";
                return path;
            }
        }

        public string LocalDataExcel
        {
            get
            {
                var path = OriginalRootDir + "LocalDataExcel/";
                return path;
            }
        }

        public string Protobuf
        {
            get
            {
                var path = OriginalRootDir + "Protobuf/";
                return path;
            }
        }

        #endregion

        public string ResourcesDir
        {
            get
            {
                var path = m_AppModule.RootDir + "Resources/";
                return path;
            }
        }

        #region AssetBundle

        /// <summary>
        /// 用于开发的本地Http服务器地址。
        /// </summary>
        /// <value>The local http root dir.</value>
        public string LocalHttpRootDir
        {
            get
            {
                var path = string.Format("{0}/AiukHttp/{1}/{2}/",
                                         Application.dataPath,
                                         m_AppModule.AppName,
                                         m_AppModule.Token);

                return path;
            }
        }

        public string AssetBundleMainDir
        {
            get
            {
                var path = string.Format("{0}AssetBundle/{1}_Main/",
                                         LocalHttpRootDir, m_AppModule.Token);
                return path;
            }
        }

        public string SandboxDir
        {
            get
            {
                var path = Application.persistentDataPath +
                                      string.Format("/{0}/{1}/", m_AppModule.AppName,
                                                    m_AppModule.Token);

                return path;
            }
        }


        #endregion

        #endregion

        #region AssetBundle

        /// <summary>
        /// 应用模块AssetBundleManifest映射字典。
        /// </summary>
        private static readonly Dictionary<string, AssetBundleManifest> ManiFestMap
        = new Dictionary<string, AssetBundleManifest>();

        public static AssetBundleManifest GetMainfest(AiukAppModuleSetting module)
        {
            if (ManiFestMap.ContainsKey(module.Token))
            {
                return ManiFestMap[module.Token];
            }

            var helper = new AiukAppModuleHelper(module);
            string path = string.Empty;

            if (AiukUnityUtility.IsEditorMode)
            {
                path = helper.AssetBundleMainDir +
                             string.Format("{0}_Main", module.Token);
            }

            if (AiukUnityUtility.IsPlayer)
            {
                path = helper.SandboxDir + string.Format("AssetBundle/{0}_Main/",
                                                         module.Token);
            }

            var manifest = AssetBundle.LoadFromFile(path).
                                      LoadAsset("AssetBundleManifest") as AssetBundleManifest;
            ManiFestMap.Add(module.Token, manifest);
            return manifest;
        }

        #endregion
    }
}


