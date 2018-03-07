#region Head

// Author:        liuruoyu1981
// CreateDate:    3/6/2018 8:52:14 AM
// Email:         liuruoyu1981@gmail.com

#endregion

using System.Collections.Generic;
using UnityEngine;

namespace AiukUnityRuntime
{
    public class AiukTexture2DRef : AiukAbsAssetRef<Texture2D>
    {
        public AiukTexture2DRef(Texture2D asset, List<AiukAssetBundleRef> assetBundleRefs)
            : base(asset, assetBundleRefs)
        {
        }

        public override void Destroy()
        {

        }
    }
}
