 

#region Head

// Author:        liuruoyu1981
// CreateDate:    3/5/2018 12:43:58 PM
// Email:         liuruoyu1981@gmail.com

#endregion

namespace AiukUnityRuntime
{
    /// <summary>
    /// 视图行为类型。
    /// </summary>
    public enum AiukViewActionType : byte
    {
        /// <summary>
        /// 视图生命周期
        /// </summary>
        Pipeline,

        /// <summary>
        /// 控件交互行为
        /// </summary>
        Interactive,

        /// <summary>
        /// 绘制及动画效果
        /// </summary>
        Animation,

        /// <summary>
        /// 模板
        /// </summary>
        Template,
    }
}
