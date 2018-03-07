//using Aiuk.Common.PoolCache;
//using AiukUnityRuntime.Core.DataPipeline;

//namespace AiukUnityRuntime
//{
//    /// <summary>
//    /// 被动数据。
//    /// </summary>
//    public interface IAiukPassiveData<TData> : IAiukRecycle
//    {
//        /// <summary>
//        /// 最近的数据访问者。
//        /// </summary>
//        IAiukDataVisitor<TData> Visitor { get; }

//        /// <summary>
//        /// 数据是否可以被清除或者回收。
//        /// </summary>
//        bool Dirty { get; }

//        /// <summary>
//        /// 将数据设置为脏，数据将在之后被清除或回收。
//        /// </summary>
//        /// <param name="visitor"></param>
//        void SetDirty(IAiukDataVisitor<TData> visitor);

//    }
//}

