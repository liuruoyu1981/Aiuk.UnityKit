 
 

#region Head

// Author:        liuruoyu1981
// CreateDate:    3/5/2018 1:25:30 PM
// Email:         liuruoyu1981@gmail.com

#endregion

namespace AiukUnityRuntime
{
    /// <summary>
    /// 视图元素。
    /// 完整视图，视图碎片，视图控件都属于视图元素。
    /// </summary>
    public interface IAiukViewlement
    {
        /// <summary>
        /// 视图ID。
        /// </summary>
        string ControlContainerId { get; }
    }
}
