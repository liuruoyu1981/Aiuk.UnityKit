#region Head

// Author:        liuruoyu1981
// CreateDate:    3/6/2018 8:40:27 AM
// Email:         liuruoyu1981@gmail.com

#endregion

using System.Collections.Generic;
using UnityEngine;

namespace AiukUnityRuntime
{
    /// <summary>
    /// ��ͼ���á�
    /// �������ͼָ���������Ľ���Ԥ�Ƽ���
    /// </summary>
    public class AiukViewRef : AiukAbsAssetRef<GameObject>
    {
        public AiukViewRef(GameObject asset, List<AiukAssetBundleRef> assetBundleRefs)
            : base(asset, assetBundleRefs)
        {
        }

        public override void Destroy()
        {
        }
    }
}
