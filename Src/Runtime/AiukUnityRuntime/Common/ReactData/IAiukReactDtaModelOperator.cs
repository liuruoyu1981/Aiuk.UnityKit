#region Head

// Author:        liuruoyu1981
// CreateDate:    3/7/2018 10:18:05 AM
// Email:         liuruoyu1981@gmail.com || 35490136@qq.com

#endregion

namespace AiukUnityRuntime
{
    /// <summary>
    /// 响应式数据模型操作器。
    /// 负责接收具体的消息实例并操作对应的响应式数据模型。
    /// 该接口的具体实现由框架自动生成，无需手动实现。
    /// </summary>
    public interface IAiukReactDtaModelOperator<TMessage, TModel>
        where TMessage : class
        where TModel : IAiukReactDataModel
    {
        /// <summary>
        /// 接收一个消息并处理对应的数据模型。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="model"></param>
        void ChangeModel(TMessage message, TModel model);
    }
}
