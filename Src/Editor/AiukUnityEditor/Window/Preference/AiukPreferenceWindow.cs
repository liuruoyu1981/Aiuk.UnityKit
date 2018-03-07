using AuikEditor;
using UnityEditor;
using UnityEngine;

public class AiukPreferenceWindow
{
    #region Field

    /// <summary>
    /// Auik开发工具包的所在目录（unityAssets目录的相对目录）
    /// </summary>
    private static readonly AiukEditorPrefsStringGuiItem AiukLocation =
        new AiukEditorPrefsStringGuiItem(AuikEditorPrefsKeys.AUIK_LOCATION, "Aiuk Location", "Aiuk");

    #endregion

    [PreferenceItem("Aiuk")]
    public static void AiukPreference()
    {
        AiukLocation.Draw();

        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("Version 1.0", EditorStyles.centeredGreyMiniLabel);
    }

}
