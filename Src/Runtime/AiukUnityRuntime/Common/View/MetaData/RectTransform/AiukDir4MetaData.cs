namespace AiukUnityRuntime.View
{
    /// <summary>
    /// 四向位置元数据。
    /// 保存一个视图控件的上、下、左、右。
    /// </summary>
    public class AiukDir4MetaData
    {
        public float Top { get; private set; }

        public float Bottom { get; private set; }

        public float Left { get; private set; }

        public float Right { get; private set; }

        public AiukDir4MetaData(float top, float bottom,
            float left, float right)
        {
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
        }
    }
}
