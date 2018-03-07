using System;


namespace AiukUnityRuntime.View
{
    /// <summary>
    /// 普通图片（精灵）元数据。
    /// </summary>
    [Serializable]
    public class AiukImageMetaData
    {
        /// <summary>
        /// 原始精灵图片名。
        /// </summary>
        public string SourceImage { get; private set; }

        public bool RaycastTarget { get; private set; }


        public AiukImageMetaData()
        {
            RaycastTarget = true;
        }


    }
}
