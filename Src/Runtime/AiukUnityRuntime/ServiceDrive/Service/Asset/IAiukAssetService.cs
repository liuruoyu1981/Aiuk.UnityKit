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
    /// ��Դ����
    /// 1. ��̬����ָ��Ҫ���ص�Ŀ����Դ����Ϸ��װ��һͬ������豸�е���Դ��
    /// 2. ��̬����ָ��Ҫ���ص�Ŀ����Դ����ҵ���߼��������ط���̬��ȡ����Դ��
    /// </summary>
    public interface IAiukAssetService
    {
        #region ��̬����

        #region ��̬ͬ������

        AiukViewRef SyncLoadView(string assetName);

        AiukMusicRef SyncLoadMusic(string assetName);

        AiukTextAssetRef SyncLoadTextAsset(string assetName);

        AiukTexture2DRef SyncLoadTexture2D(string assetName);

        AiukSoundRef SyncLoadSound(string assetName);

        List<T> SyncLoadAll<T>(string assetName) where T : UnityEngine.Object;

        #endregion

        #region ��̬�첽����

        void SyncLoadView(string assetName, Action<AiukViewRef> callback);

        void SyncLoadMusic(string assetName, Action<AiukMusicRef> callback);

        void SyncLoadTextAsset(string assetName, Action<AiukTextAssetRef> callback);

        void SyncLoadTexture2D(string assetName, Action<AiukTexture2DRef> callback);

        void SyncLoadSound(string assetName, Action<AiukSoundRef> callback);

        #endregion

        #endregion

        #region ��̬����

        #region ��̬�첽����




        #endregion


        #region ��̬ͬ������




        #endregion



        #endregion
    }
}
