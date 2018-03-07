using System;

namespace AuikEditor
{
    /// <summary>
    /// 可以绘制自身的编辑器基本值类型。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AuikAbsEditorPrefsItem<T>
    {
        public string Key;
        public string Label;
        public T DefaultValue;

        public AuikAbsEditorPrefsItem(string key, string label, T defaultValue)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            Key = key;
            Label = label;
            DefaultValue = defaultValue;
        }

        public abstract T Value { get; set; }
        public abstract void Draw();

        public static implicit operator T(AuikAbsEditorPrefsItem<T> s)
        {
            return s.Value;
        }
    }
}

