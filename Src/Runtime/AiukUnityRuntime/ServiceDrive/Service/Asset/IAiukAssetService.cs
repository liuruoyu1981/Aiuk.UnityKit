#region Head

// Author:        liuruoyu1981
// CreateDate:    3/6/2018 8:15:38 AM
// Email:         liuruoyu1981@gmail.com

#endregion

using System;
using System.Collections.Generic;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 资源服务。
    /// 1. 静态加载指需要加载的目标资源随游戏安装包一同打包进设备中的资源。
    /// 2. 动态加载指需要加载的目标资源是随业务逻辑从其他地方动态获取的资源。
    /// </summary>
    public interface IAiukAssetService
    {
        #region 静态加载

        #region 静态同步加载

        AiukViewRef SyncLoadView(string assetName);

        AiukMusicRef SyncLoadMusic(string assetName);

        AiukTextAssetRef SyncLoadTextAsset(string assetName);

        AiukTexture2DRef SyncLoadTexture2D(string assetName);

        AiukSoundRef SyncLoadSound(string assetName);

        List<T> SyncLoadAll<T>(string assetName) where T : UnityEngine.Object;

        #endregion

        #region 静态异步加载

        void SyncLoadView(string assetName, Action<AiukViewRef> callback);

        void SyncLoadMusic(string assetName, Action<AiukMusicRef> callback);

        void SyncLoadTextAsset(string assetName, Action<AiukTextAssetRef> callback);

        void SyncLoadTexture2D(string assetName, Action<AiukTexture2DRef> callback);

        void SyncLoadSound(string assetName, Action<AiukSoundRef> callback);

        #endregion

        #endregion

        #region 动态加载

        #region 动态异步加载




        #endregion


        #region 动态同步加载




        #endregion



        #endregion
    }
}
