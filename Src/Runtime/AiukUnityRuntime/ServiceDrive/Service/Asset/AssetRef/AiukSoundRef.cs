#region Head

// Author:        liuruoyu1981
// CreateDate:    3/6/2018 11:25:08 AM
// Email:         liuruoyu1981@gmail.com

#endregion

using System.Collections.Generic;
using UnityEngine;

namespace AiukUnityRuntime
{
    public class AiukSoundRef : AiukAbsAssetRef<AudioClip>
    {
        public AiukSoundRef(AudioClip asset, List<AiukAssetBundleRef> assetBundleRefs) 
            : base(asset, assetBundleRefs)
        {
        }

        public override void Destroy()
        {
            
        }
    }
}
