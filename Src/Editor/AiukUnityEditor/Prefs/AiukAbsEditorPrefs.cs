using Aiuk.Unity.Common;

/// <summary>
/// 基础编辑器持久化数据。
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class AiukAbsEditorPrefs<T> : IAiukPrefs<T>
{
    /// <summary>
    /// 默认值不可变。
    /// </summary>
    public T DefaultValue { get; private set; }

    /// <summary>
    /// 数据的Key值。
    /// </summary>
    public string Key { get; private set; }

    protected string FinalKey
    {
        get { return "AiukEditorPrefs_" + Key; }
    }

    public abstract T GetValue();

    public void InitPrefs(string key, T defaultValue)
    {
        Key = key;
        DefaultValue = defaultValue;
    }

    public abstract void SetValue(T value);

}
