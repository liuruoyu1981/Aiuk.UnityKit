using UnityEngine;
using UnityEditor;
using System.Linq;

namespace DeadMosquito.Revealer
{
    /// <summary>
    /// Assets窗口扩展。
    /// 1. 快速刷新（点击附加按钮直接调用框架刷新功能。）
    ///     a. 刷新框架必备的脚本项、创建框架必备的资源项。
    ///     b. 刷新当前App（开发项目）的必备脚本即资源项。
    /// </summary>
    [InitializeOnLoad]
    public static class AiukAssetsExtension
    {
        private static Texture2D _darkSkinTex;
        private static Texture2D _lightSkinTex;
        private static Texture2D _lightRefreshTex;
        private static float IconSize = EditorGUIUtility.singleLineHeight;

        private const int Offset = 1;

        static AiukAssetsExtension()
        {
            LoadTextures();

            EditorApplication.projectWindowItemOnGUI += HandleProjectWindowItemOnGUI;
        }

        private static void LoadTextures()
        {
            _darkSkinTex = AssetDatabase.LoadAssetAtPath<Texture2D>
                ("Assets/Aiuk.UnityKit/Src/Editor/AiukUnityEditor/AssetsExtension/Asset/reveal-light.png");
            _lightSkinTex = AssetDatabase.LoadAssetAtPath<Texture2D>
                ("Assets/Aiuk.UnityKit/Src/Editor/AiukUnityEditor/AssetsExtension/Asset/reveal-dark.png");
            _lightRefreshTex = AssetDatabase.LoadAssetAtPath<Texture2D>
                ("Assets/Aiuk.UnityKit/Src/Editor/AiukUnityEditor/AssetsExtension/Asset/refresh_light.png");
        }

        private static void HandleProjectWindowItemOnGUI(string guid, Rect rect)
        {
            AddRefreshIcon(guid, rect);
            AddOpenFolder(guid, rect);
            EditorApplication.RepaintProjectWindow();
        }

        private static void AddOpenFolder(string guid, Rect rect)
        {
            var isHover = rect.Contains(Event.current.mousePosition) && RevealerEditorSettings.ShowOnHover;
            var isSelected = IsSelected(guid) && RevealerEditorSettings.ShowOnSelected;

            var isVisible = isHover || isSelected;

            if (!isVisible)
            {
                return;
            }

            var iconSize = EditorGUIUtility.singleLineHeight;
            var iconRect = new Rect(rect.width + rect.x - iconSize - RevealerEditorSettings.OffsetInProjectView, rect.y,
                iconSize - Offset, iconSize - Offset);

            GUI.DrawTexture(iconRect, GetTex());

            var path = AssetDatabase.GUIDToAssetPath(guid);
            if (GUI.Button(iconRect, GUIContent.none, GUIStyle.none))
            {
                EditorUtility.RevealInFinder(path);
            }
        }

        private static void AddRefreshIcon(string guid, Rect rect)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var iconRect = new Rect(rect.width + rect.x - IconSize - 220, rect.y,
                IconSize - Offset, IconSize - Offset);

            if (!path.EndsWith("Aiuk.UnityKit")) return;

            GUI.DrawTexture(iconRect, _lightRefreshTex);
            if (GUI.Button(iconRect, GUIContent.none, GUIStyle.none))
            {
                Debug.Log("Refresh");
            }
        }

        private static Texture2D GetTex()
        {
            if (_darkSkinTex == null || _lightSkinTex == null)
            {
                LoadTextures();
            }

            return EditorGUIUtility.isProSkin ? _darkSkinTex : _lightSkinTex;
        }

        private static bool IsSelected(string guid)
        {
            return Selection.assetGUIDs.Any(guid.Contains);
        }
    }
}