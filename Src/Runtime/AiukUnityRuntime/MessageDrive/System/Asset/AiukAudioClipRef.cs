using System.Collections.Generic;
using UnityEngine;

namespace AiukUnityRuntime.System.Asset
{
    public class AiukAudioClipRef : AiukAbsAssetRef<AudioClip>
    {
        public AiukAudioClipRef(AudioClip asset, List<AiukAssetBundleRef> abRefs) : base(asset, abRefs)
        {
        }

        public override void Release()
        {
        }
    }
}

