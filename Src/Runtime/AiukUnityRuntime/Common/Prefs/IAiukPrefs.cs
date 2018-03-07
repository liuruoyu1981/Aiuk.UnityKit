namespace Aiuk.Unity.Common
{
    public interface IAiukPrefs<T>
    {
        void SetValue(T value);

        T GetValue();

        void InitPrefs(string key, T defaultValue);
    }
}
