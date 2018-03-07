namespace AiukUnityRuntime.View
{
    /// <summary>
    /// 锚点对齐类型。
    /// </summary>
    public enum AiukAnchorType : byte
    {
        #region Left

        /// <summary>
        /// 居中左对齐。
        /// 4向锚点取值为
        /// 0 0.5
        /// 0 0.5
        /// </summary>
        LeftMiddle,

        /// <summary>
        /// 靠上左对齐。
        /// 4向锚点取值为
        /// 0 1
        /// 0 1
        /// </summary>
        LeftTop,

        /// <summary>
        /// 靠下左对齐。
        /// 4向锚点取值为
        /// 0 0
        /// 0 0
        /// </summary>
        LeftBottom,

        /// <summary>
        /// 左对齐。
        /// 4向锚点取值为
        /// 0 0
        /// 0 1
        /// </summary>
        Left,

        #endregion

        #region Center

        /// <summary>
        /// 上下对齐。
        /// 4向锚点取值为
        /// 0.5 0
        /// 0.5 1
        /// </summary>
        Center,

        /// <summary>
        /// 上居中对齐。
        /// 4向锚点取值为
        /// 0.5 1
        /// 0.5 1
        /// </summary>
        CenterTop,

        /// <summary>
        /// 完全居中对齐。
        /// 4向锚点取值为
        /// 0.5 0.5
        /// 0.5 0.5
        /// </summary>
        CenterMiddle,

        /// <summary>
        /// 下居中对齐。
        /// 4向锚点取值为
        /// 0.5 0
        /// 0.5 0
        /// </summary>
        CenterBottom,

        #endregion

        #region Right

        /// <summary>
        /// 右对齐。
        /// 4向锚点取值为
        /// 1 0
        /// 1 1
        /// </summary>
        Right,

        /// <summary>
        /// 右上对齐。
        /// 4向锚点取值为
        /// 1 1
        /// 1 1
        /// </summary>
        RightTop,

        /// <summary>
        /// 右靠中对齐。
        /// 4向锚点取值为
        /// 1 0.5
        /// 1 0.5
        /// </summary>
        RightMiddle,

        /// <summary>
        /// 右靠下对齐。
        /// 4向锚点取值为
        /// 1 0
        /// 1 0
        /// </summary>
        RightBottom,

        #endregion

        #region Other

        /// <summary>
        /// 上对齐。
        /// 4向锚点取值为
        /// 0 1
        /// 1 1
        /// </summary>
        Top,

        /// <summary>
        /// 中对齐。
        /// 4向锚点取值为
        /// 0 0.5
        /// 1 0.5
        /// </summary>
        Middle,

        /// <summary>
        /// 下对齐。
        /// 4向锚点取值为
        /// 1 0
        /// 1 0
        /// </summary>
        Bottom,

        /// <summary>
        /// 四向对齐。
        /// 4向锚点取值为
        /// 0 0
        /// 1 1
        /// </summary>
        Dir4,

        #endregion
    }
}

