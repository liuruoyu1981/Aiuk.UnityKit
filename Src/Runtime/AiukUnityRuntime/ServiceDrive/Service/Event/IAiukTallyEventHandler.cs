﻿namespace AiukUnityRuntime
{
    #region 非泛型

    /// <summary>
    /// 无需数据的可计数事件处理器。
    /// </summary>
    public interface IAiukTallyEventHandler : IAiukEventHandler
    {
        /// <summary>
        /// 事件剩余的执行次数。
        /// </summary>
        /// <value>The residue count.</value>
        int ResidueCount { get; }



    }

    #endregion

    #region 泛型

    /// <summary>
    /// 需求一个数据的可计数事件处理器。
    /// </summary>
    public interface IAiukTallyEventHandler<TData> : IAiukEventHandler<TData>
    {
        /// <summary>
        /// 事件剩余的执行次数。
        /// </summary>
        /// <value>The residue count.</value>
        int ResidueCount { get; }
    }

    #endregion


}

