#region Head

// Author:        liuruoyu1981
// CreateDate:    3/7/2018 10:11:32 AM
// Email:         liuruoyu1981@gmail.com || 35490136@qq.com

#endregion

using System.Collections.Generic;

namespace AiukUnityRuntime
{
    /// <summary>
    /// 响应式数据模型。
    /// 标识接口。
    /// 实现该接口的对象其字段必须为IAiukReactData的具体实现。
    /// </summary>
    public interface IAiukReactDataModel
    {
        /// <summary>
        /// 数据所关注的消息协议的Id编号列表。
        /// 每当列表中的消息到达时，模型都会收到通知。
        /// </summary>
        List<int> ConcernedProtoIds { get; }

        /// <summary>
        /// 和给定的应用实例建立绑定。
        /// </summary>
        /// <param name="app"></param>
        void BingdingApp(IAiukUnityApp app);
    }
}
