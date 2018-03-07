using System;

namespace AiukUnityRuntime.View
{
    /// <summary>
    /// 二维位置元数据。
    /// </summary>
    [Serializable]
    public class AiukVector2MetaData
    {
        public float X { get; private set; }

        public float Y { get; private set; }

        public AiukVector2MetaData Init(float x, float y)
        {
            X = x;
            Y = y;

            return this;
        }
    }
}


