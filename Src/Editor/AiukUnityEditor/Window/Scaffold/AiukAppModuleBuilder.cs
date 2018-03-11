using Aiuk.Common.Utility;
using AiukUnityRuntime;
using UnityEditor;

namespace AiukUnityEditor
{
    /// <summary>
    /// AppModule构建器。
    /// </summary>
    public class AiukAppModuleBuilder
    {
        private readonly AiukAppModuleHelper m_PahtHelper;
        private readonly AiukAppModuleSetting m_AppModule;

        public AiukAppModuleBuilder(AiukAppModuleSetting moduleSetting)
        {
            m_AppModule = moduleSetting;
            m_PahtHelper = new AiukAppModuleHelper(moduleSetting);
        }

        private void CreateAppModuleCsDir()
        {
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.CsRootDir);
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.CsNetRequestDir);
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.CsNetResponseDir);
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.CsDllDir);
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.CsEditorDir);
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.CsAiukUnityKitOverrideDir);
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.CsLocalDataDir);
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.CsMonoEntitesDir);
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.CsMvdaDir);
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.CsReactiveDataDir);
        }

        private void CrewateAssetDatabaseDir()
        {
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.AssetDatabaseRootDir);
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.AssetDatabaseAtlasDir);
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.AssetDatabaseBinaryDir);
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.AssetDatabaseFontDir);
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.CsLocalDataDir);
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.AssetDatabaseMusicDir);
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.AssetDatabaseMaterial);
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.AssetDatabaseSoundEffect);
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.AssetDatabaseShader);
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.AssetDatabaseView);
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.AssetDatabaseTexture);
        }

        private void CreateOriginalAssetDir()
        {
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.OriginalRootDir);
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.AtlasSpriteDir);
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.AtlasSplitDir);
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.LocalDataExcel);
            AiukIOUtility.TryCreateDirectory(m_PahtHelper.Protobuf);
        }

        public void BuildAppModule()
        {
            CreateAppModuleCsDir();
            CrewateAssetDatabaseDir();
            CreateOriginalAssetDir();

            AiukDebugUtility.Log("更新成功",
                string.Format("应用模块{0}的文件结构及资源已更新。", m_AppModule.Name),
                "知道了");
        }
    }
}