 
 

#region Head

// Author:        liuruoyu1981
// CreateDate:    3/5/2018 1:47:40 PM
// Email:         liuruoyu1981@gmail.com

#endregion

namespace AiukUnityRuntime
{
    /// <summary>
    /// 视图行为处理代理。
    /// 用于在Ts脚本层和C#脚本层之间增加一个中间对象，判断对应的请求是否由对应的C#响应者以及是否存在对应的Ts响应器。
    /// 如果存在Ts响应器，则不管C#响应器是否存在，都将优先执行Ts响应器。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AiukViewActionAgent<T> where T : IAiukViewlement
    {
        #region 字段




        #endregion
    }
}
