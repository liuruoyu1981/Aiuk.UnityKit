using UnityEditor;

namespace AiukUnityEditor
{
    public class AiukEditorStringPrefs : AiukAbsEditorPrefs<string>
    {
        public override void SetValue(string value)
        {
            var exist = EditorPrefs.GetString(FinalKey);
            if (!string.IsNullOrEmpty(exist))
            {
                EditorPrefs.SetString(FinalKey, value);
            }
        }

        public override string GetValue()
        {
            return EditorPrefs.GetString(FinalKey);
        }

    }
}

