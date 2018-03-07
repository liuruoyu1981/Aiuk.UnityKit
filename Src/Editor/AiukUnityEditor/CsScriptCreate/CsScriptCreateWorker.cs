/// <summary>
/// 普通Cs脚本创建器劳工。
/// 负责依据构建窗口所传递的脚本创建数据自动分析目标脚本的代码结构、文本内容及执行最终文件写入操作。
/// </summary>
[Worker(true)]
public class CsScriptCreateWorker : ICancelWorker
{
    public WorkState WorkState { get; private set; }
    public void Work()
    {
    }

    public void Cancel()
    {
    }
}
