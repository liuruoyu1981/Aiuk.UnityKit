using System;
using UnityEngine;

namespace AiukUnityRuntime.View
{
    /// <summary>
    /// RectTransform组件元数据。
    /// </summary>
    [Serializable]
    public class AiukRectTransformMetaData
    {
        /// <summary>
        /// 四向的相对距离。
        /// </summary>
        public AiukDir4MetaData Dir4 { get; private set; }

        public float PosZ { get; private set; }

        public AiukVector2MetaData AnchorMin { get; private set; }

        public AiukVector2MetaData AnchorMax { get; private set; }

        public AiukVector2MetaData Pivot { get; private set; }

        public AiukRotationMetaData Rotation { get; private set; }

        public AiukScaleMetaData Scale { get; private set; }

        public AiukRectTransformMetaData(RectTransform rect)
        {

        }
    }
}

