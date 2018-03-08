using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using Aiuk.Common.Utility;
using AiukUnityEditor;
using AiukUnityRuntime;

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
        private static Texture2D _lightMenuTex;
        private static Texture2D _lightAppTex;
        private static readonly float IconSize = EditorGUIUtility.singleLineHeight;
        private static readonly Dictionary<string, IAiukEditorAssetsFuction> AssetsFunctions
        = new Dictionary<string, IAiukEditorAssetsFuction>();

        private const int Offset = 1;

        private static void InitAssetsFunction()
        {
            var functionTypes =
                AiukReflectUtility.GetTypeList<IAiukEditorAssetsFuction>
                    (false, false, AiukUnityUtility.AssemblyCSharpEditor);
            var tempFunctions = new List<IAiukEditorAssetsFuction>();

            foreach (var functionType in functionTypes)
            {
                var function = Activator.CreateInstance(functionType) as IAiukEditorAssetsFuction;
                tempFunctions.Add(function);
            }

            tempFunctions = tempFunctions.OrderBy(f => f.MenuTitle).ToList();
            foreach (var assetsFuction in tempFunctions)
            {
                AssetsFunctions.Add(assetsFuction.MenuTitle, assetsFuction);
            }
        }

        static AiukAssetsExtension()
        {
            InitAssetsFunction();
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
            _lightMenuTex = AssetDatabase.LoadAssetAtPath<Texture2D>
             ("Assets/Aiuk.UnityKit/Src/Editor/AiukUnityEditor/AssetsExtension/Asset/menu_light.png");
            _lightAppTex = AssetDatabase.LoadAssetAtPath<Texture2D>
                ("Assets/Aiuk.UnityKit/Src/Editor/AiukUnityEditor/AssetsExtension/Asset/app_light.png");
        }

        private static void HandleProjectWindowItemOnGUI(string guid, Rect rect)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            AddRefreshIcon(path, rect);
            AddMenuIcon(path, rect);
            AddAppIcon(path, rect);
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

        private static void AddRefreshIcon(string path, Rect rect)
        {
            //var path = AssetDatabase.GUIDToAssetPath(guid);
            var iconRect = new Rect(rect.width + rect.x - IconSize - 31, rect.y,
                IconSize - Offset, IconSize - Offset);

            if (!path.EndsWith("Aiuk.UnityKit")) return;

            //GUI.DrawTexture(iconRect, _lightRefreshTex);
            var content = new GUIContent(_lightRefreshTex, "调用所有重建任务");
            if (!GUI.Button(iconRect, content, GUIStyle.none)) return;
        }

        private static void AddMenuIcon(string path, Rect rect)
        {
            //var path = AssetDatabase.GUIDToAssetPath(guid);
            var iconRect = new Rect(rect.width + rect.x - IconSize - 15, rect.y,
                IconSize - Offset, IconSize - Offset);

            if (!path.EndsWith("Aiuk.UnityKit")) return;

            //GUI.DrawTexture(iconRect, _lightMenuTex);
            var content = new GUIContent(_lightMenuTex, "呼出框架菜单");
            if (GUI.Button(iconRect, content, GUIStyle.none))
            {
                var menu = new GenericMenu();

                foreach (var kv in AssetsFunctions)
                {
                    menu.AddItem(new GUIContent(kv.Value.MenuTitle), false, Callback,
                        kv.Value.MenuTitle);
                }
                menu.ShowAsContext();
            }
        }

        private static void AddAppIcon(string path, Rect rect)
        {
            var iconRect = new Rect(rect.width + rect.x - IconSize - 15, rect.y,
                IconSize - Offset, IconSize - Offset);

            if (!path.EndsWith("ThreeKK")) return;
            var content = new GUIContent(_lightAppTex, "呼出应用菜单");
            if (GUI.Button(iconRect, content, GUIStyle.none))
            {
            }
        }

        private static void Callback(object obj)
        {
            var title = (string)obj;
            var function = AssetsFunctions[title];
            function.Execute();
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