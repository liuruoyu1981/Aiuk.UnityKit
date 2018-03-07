using UnityEditor;

namespace AuikEditor
{
    /// <summary>
    /// 可以绘制自身字符串值的编辑器设置类型。
    /// </summary>
    public class AiukEditorPrefsStringGuiItem : AuikAbsEditorPrefsItem<string>
    {
        public AiukEditorPrefsStringGuiItem(string key, string label, string defaultValue)
            : base(key, label, defaultValue)
        {
            EditorPrefs.SetString(Key, DefaultValue);
            m_PrevValue = DefaultValue;
        }

        private string m_PrevValue;
        public override string Value
        {
            get { return m_PrevValue; }
            set
            {
                if (value == m_PrevValue) return;

                m_PrevValue = value;
                EditorPrefs.SetString(Key, value);
            }
        }

        public override void Draw()
        {
            Value = EditorGUILayout.TextField(Label, Value);
        }
    }
}
