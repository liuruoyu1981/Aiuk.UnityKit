//using System;
//using System.Collections.Generic;
//using System.Linq;
//using AiukUnityRuntime;
//using AuikEditor;
//using UnityEditor;
//using UnityEngine;
//using Object = UnityEngine.Object;

//namespace AiukUnityEditor
//{
//    /// <summary>
//    /// 闪电窗口，用于作为Auik框架的快速启动器快速操作各种功能。
//    /// </summary>
//    public class AiukLightingWindow : EditorWindow
//    {
//        #region Unity内建类型集合

//        private static IEnumerable<Type> UnityTypes;

//        private static readonly List<AiukBuiltInAssetType> BuiltInAssetTypes = new List<AiukBuiltInAssetType>
//        {
//            new AiukBuiltInAssetType("AnimationClip", typeof(Animation)),
//            new AiukBuiltInAssetType("AudioClip", typeof(AudioClip)),
//#if (UNITY_5)
//            new AiukBuiltInAssetType("AudioMixer", typeof(AudioMixer)),
//#endif
//            new AiukBuiltInAssetType("Font", typeof(Font)),
//            new AiukBuiltInAssetType("GUISkin", typeof(GUISkin)),
//            new AiukBuiltInAssetType("Material", typeof(Material)),
//            new AiukBuiltInAssetType("Mesh", typeof(Mesh)),
//            new AiukBuiltInAssetType("Model", typeof(GameObject), ".fbx", ".dae", ".3ds", ".dxf", ".obj"),
//            new AiukBuiltInAssetType("PhysicMaterial", typeof(PhysicMaterial)),
//            new AiukBuiltInAssetType("Prefab", typeof(GameObject), ".prefab"),
//            new AiukBuiltInAssetType("Scene", typeof(UnityEngine.Object), ".unity"),
//            new AiukBuiltInAssetType("Script", typeof(MonoScript)),
//            new AiukBuiltInAssetType("Shader", typeof(Shader)),
//            new AiukBuiltInAssetType("Sprite", typeof(Sprite)),
//            new AiukBuiltInAssetType("Texture", typeof(Texture))
//        };

//        #endregion

//        #region 启动菜单项

//        [MenuItem("Aiuk/Lighint/Search Asset #k")]
//        private static void SearchAsset()
//        {
//            //ShowWindow(new Aiukmo
//            //{
//            //    RefreshAction = null,
//            //    SearchLabel = "Enter Asset Name:",
//            //    LoadItem = LoadAsset
//            //});
//        }

//        /// <summary>
//        /// 搜索游戏对象。
//        /// </summary>
//        [MenuItem("Aiuk/Lighint/Search GameObject #g")]
//        private static void SearchGameObject()
//        {
//        }

//        /// <summary>
//        /// 显示最近的搜索结果。
//        /// </summary>
//        [MenuItem("Aiuk/Lighint/Show RecentItems #e")]
//        private static void ShowRecentItems()
//        {
//        }

//        #endregion

//        #region 搜索模式所用委托

//        private static Object[] LoadAsset(AiukLightingInfo info)
//        {
//            var parentPath = info.ParentPath.Length == 0 ? "" : info.ParentPath + "/";
//            var result = new[] { AssetDatabase.LoadAssetAtPath("Assets/" + parentPath + info.Name, typeof(Object)) };
//            return result;
//        }

//        private static void InitUnityTypes()
//        {
//            if (UnityTypes == null)
//            {
//                // 返回Unity程序集（运行时及编辑器）下所有Unity.Object的子类。
//                UnityTypes = AppDomain.CurrentDomain.GetAssemblies()
//                    .SelectMany(asm => asm.GetTypes())
//                    .Where(t =>
//                        t != null && t.IsClass && t.Namespace != null
//                        && (t.Namespace.StartsWith("UnityEngine") || t.Namespace.StartsWith("UnityEditor"))
//                        && typeof(Object).IsAssignableFrom(t)).ToList();
//            }
//        }

//        #endregion

//        /// <summary>
//        /// 当前选中项的名字。
//        /// </summary>
//        private string m_ItemName = "";

//        /// <summary>
//        /// 之前选中项的名字。
//        /// </summary>
//        private string m_PrevItemName;

//        /// <summary>
//        /// 默认的无结果数组。
//        /// </summary>
//        private static readonly AiukLightingInfo[] NoResults = { };

//        private IEnumerable<AiukLightingInfo> m_ItemInfos = NoResults;

//        private void OnEnable()
//        {
//            InitStyle();
//            InitLeftTabsContext();
//        }

//        #region 样式

//        /// <summary>
//        /// 开启富文本支持并使用右居中对齐的编辑器样式
//        /// </summary>
//        private GUIStyle RightAlignRichTextGuiStyle;

//        /// <summary>
//        /// 启用富文本支持的编辑器样式
//        /// 字体大小为12，文本颜色为默认颜色。
//        /// </summary>
//        private GUIStyle RichTextGuiStyle;

//        private GUIStyle CenteredLabelStyle;

//        private Texture2D m_SelectedLineBackgroundTex;
//        private Texture2D m_SearchLineBackgroundTex;

//        private GUIStyle m_SeletedLineGuiStyle;
//        private GUIStyle m_SearchLineGuiStyle;
//        private GUIStyle m_RegularLineStyle;

//        private bool m_IsDardSkin;

//        private void InitStyle()
//        {
//            m_IsDardSkin = IsDarkSkin();

//            RichTextGuiStyle = new GUIStyle
//            {
//                richText = true,
//                fontSize = 12,
//                margin = new RectOffset(5, 5, 5, 5),
//                normal = { textColor = EditorStyles.label.normal.textColor }
//            };

//            RightAlignRichTextGuiStyle = new GUIStyle
//            {
//                richText = true,
//                fontSize = 12,
//                margin = new RectOffset(5, 5, 5, 5),
//                alignment = TextAnchor.MiddleRight,
//                normal = { textColor = new Color(.4f, .4f, .4f) }
//            };

//            CenteredLabelStyle =
//                new GUIStyle(EditorStyles.label) { alignment = TextAnchor.UpperCenter, wordWrap = true };

//            m_SearchLineBackgroundTex = MakeTex((int)WINDOW_WIDTH, 1, SelectionBackgroundColor());

//            m_SearchLineGuiStyle = new GUIStyle
//            {
//                richText = true,
//                fontSize = 12,
//                normal = { background = m_SearchLineBackgroundTex },
//                margin = new RectOffset(5, 5, 10, 10),
//            };

//            //            m_SelectedLineBackgroundTex = M
//        }

//        private Color SelectionBackgroundColor()
//        {
//            return m_IsDardSkin ? new Color(.1f, .5f, .9f, .5f) : new Color(.9f, .95f, 1f, 1f);
//        }

//        /// <summary>
//        /// 生成指定宽高、颜色的材质图片。
//        /// </summary>
//        /// <param name="width"></param>
//        /// <param name="height"></param>
//        /// <param name="color"></param>
//        /// <returns></returns>
//        private static Texture2D MakeTex(int width, int height, Color color)
//        {
//            var pix = new Color[width * height];
//            for (var i = 0; i < pix.Length; i++)
//            {
//                pix[i] = color;
//            }

//            var result = new Texture2D(width, height);
//            result.SetPixels(pix);
//            result.Apply();
//            return result;
//        }

//        #endregion

//        /// <summary>
//        /// 当前是否是编辑器专业版皮肤。
//        /// </summary>
//        /// <returns></returns>
//        private static bool IsDarkSkin()
//        {
//            var isDarkSkin = EditorStyles.label.normal.textColor.r > .5f;
//            return isDarkSkin;
//        }

//        #region LeftTabs

//        /// <summary>
//        /// 初始化左部标签上下文环境。
//        /// </summary>
//        private void InitLeftTabsContext()
//        {
//            LeftTabTitles.Add("Search");

//            var searchIcon = AssetDatabase.LoadAssetAtPath<Texture>("Assets/" +
//                                                                    EditorPrefs.GetString(AuikEditorPrefsKeys
//                                                                        .AUIK_LOCATION) + "/Asset/Icon/search.png");
//            LeftTabTextures.Add(searchIcon);
//        }

//        #endregion


//        #region private method

//        /// <summary>
//        /// 闪电窗口宽度。
//        /// </summary>
//        private const float WINDOW_WIDTH = 600;

//        /// <summary>
//        /// 闪电窗口高度。
//        /// </summary>
//        private const float WINDOW_HEIGHT = 100;

//        /// <summary>
//        /// 闪电窗口命令行分页背景颜色。
//        /// </summary>
//        private Color m_CmdColor = Color.black;

//        private int m_SelectedIndex;

//        private bool m_SelectAll;


//        /// <summary>
//        /// 闪电窗口左起始锚点X值。
//        /// </summary>
//        private const string AIUK_LIGHTING_POSITION_X = "AIUK_LIGHTING_POSITION_X";

//        /// <summary>
//        /// 闪电窗口左起始锚点Y值。
//        /// </summary>
//        private const string AIUK_LIGHTING_POSITION_Y = "AIUK_LIGHTING_POSITION_Y";

//        private static List<string> LeftTabTitles = new List<string>();
//        private static List<Texture> LeftTabTextures = new List<Texture>();

//        private static AiukLightingWindow _PrevWindow;
//        private AiukLightingMode m_Mode;

//        [MenuItem("Aiuk/Lighting %i")]
//        public static void ShowWindow(AiukLightingMode mode)
//        {
//            if (_PrevWindow != null)
//            {
//                if (_PrevWindow.m_Mode.SearchLabel == mode.SearchLabel) return;
//                _PrevWindow.Close();
//            }

//            _PrevWindow = CreateInstance<AiukLightingWindow>();
//            _PrevWindow.wantsMouseMove = true;
//            _PrevWindow.m_Mode = mode;
//            if (mode.MoveWindowMode) //    移动修改窗口位置。
//            {
//            }
//            else
//            {
//                var positionRect = PositionRect();
//                positionRect.y -= positionRect.height;
//                _PrevWindow.ShowAsDropDown(positionRect, new Vector2(WINDOW_WIDTH, WINDOW_HEIGHT));
//            }
//        }

//        private static Rect PositionRect()
//        {
//            var positionX = EditorPrefs.GetFloat(AIUK_LIGHTING_POSITION_X, (float)Screen.currentResolution.width
//                                                                           / 2 - WINDOW_WIDTH / 2);
//            var positionY = EditorPrefs.GetFloat(AIUK_LIGHTING_POSITION_Y, Screen.currentResolution.height / 2 - 100);
//            var rect = new Rect(positionX, positionY, WINDOW_WIDTH, WINDOW_HEIGHT);
//            return rect;
//        }

//        private void OnGUI()
//        {
//            if (KeyDown(KeyCode.Escape))
//            {
//                Close();
//                return;
//            }
//            DetectMouseHover();
//            if (KeyDown(KeyCode.DownArrow))
//            {
//                m_SelectAll = false;
//                m_SelectedIndex++;
//            }
//            if (KeyDown(KeyCode.UpArrow))
//            {
//                m_SelectAll = false;
//                m_SelectedIndex--;
//            }
//            if (Event.current.type == EventType.ValidateCommand && Event.current.commandName == "SelectAll")
//            {
//                m_SelectAll = !m_SelectAll;
//                Event.current.Use();
//            }
//            if (string.IsNullOrEmpty(m_ItemName))
//            {
//                m_SelectAll = false;
//            }
////            var enterPressed = KeyDown(KeyCode.Return) || KeyDown(KeyCode.Tab);
////            var shiftPressed = Event.current.shift;
////            var controlPressed = IsOSX() ? Event.current.command : Event.current.control;
////            var altPressed = Event.current.alt;
//            m_PrevItemName = m_ItemName;
//            DisplaySearchTextField();
//        }

//        private void DetectMouseHover()
//        {
//            if (Event.current.type != EventType.MouseMove) return;
//            if (Event.current.mousePosition.y < 54) return;
//            var hoverdIndex = MousePositionIndex();
//            if (hoverdIndex == m_SelectedIndex) return;
//            m_SelectAll = false;
//            m_SelectedIndex = hoverdIndex;
//            Repaint();
//        }

//        private static int MousePositionIndex()
//        {
//            return (int)(Event.current.mousePosition.y - 54) / 24;
//        }

//        private static bool IsOSX()
//        {
//            return Application.platform == RuntimePlatform.OSXEditor;
//        }

//        /// <summary>
//        /// 判断给定的键当前是否处于按下状态。
//        /// </summary>
//        /// <param name="keyCode"></param>
//        /// <returns></returns>
//        private bool KeyDown(KeyCode keyCode)
//        {
//            return Event.current.type == EventType.KeyDown && Event.current.keyCode == keyCode;
//        }

//        #endregion

//        #region 绘制

//        /// <summary>
//        /// 左部分类Tab按钮列表。
//        /// </summary>
////        private readonly List<AiukTextureButton> m_LeftTexButtons = new List<AiukTextureButton>();

//        /// <summary>
//        /// 初始化左部分类Tab按钮绘制区域列表。
//        /// </summary>
//        private void InitLeftTabsRects(List<string> tabTitles, List<Texture> tabTextures)
//        {
//            if (tabTitles.Count != tabTextures.Count) return;
//            var count = tabTitles.Count;
//            var startX = EditorPrefs.GetFloat(AIUK_LIGHTING_POSITION_X);
//            var startY = EditorPrefs.GetFloat(AIUK_LIGHTING_POSITION_Y);
//            var anyTex = tabTextures[0];
//            var width = anyTex.width;
//            var height = anyTex.height;
//            for (var i = 0; i < count; i++)
//            {
//                //var rect = new Rect(startX, startY + (i + 1) * height, width, height);
//                //var texButton = new AiukTextureButton(tabTextures[i], rect);
//                //m_LeftTexButtons.Add(texButton);
//            }
//        }

//        /// <summary>
//        /// 当前闪电分页窗口的标题。
//        /// </summary>
//        private string m_CurrentTitle = "Search Asset";

//        private void DisplaySearchTextField()
//        {
//            using (new AiukHorizontalLayout())
//            {
//                //AiukEditorLayout.LabelField(
//                //    string.Format("<b><color=#05A4C4>{0}->Shift+Arrow Switch Mode!</color></b>", m_CurrentTitle),
//                //    RichTextGuiStyle);
//                //AiukEditorLayout.LabelField("<b><color=#fa3e00>Lighting</color></b>",
//                //    RightAlignRichTextGuiStyle);
//            }

//            const string controlName = "mh_asset_name";
//            GUI.SetNextControlName(controlName);
//            m_ItemName = GUILayout
//                .TextField((m_ItemName ?? "").Replace("`", ""), m_SearchLineGuiStyle, GUILayout.ExpandWidth(true))
//                .Replace("`", "");
//            GUI.FocusControl(controlName);
//            ForceCaretToEndOfTextField();
//        }

//        private void ForceCaretToEndOfTextField()
//        {
//            var te = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), GUIUtility.keyboardControl);
//#if UNITY_4_5 || UNITY_4_6 || UNITY_5_0 || UNITY_5_1
//            te.pos = m_ItemName.Length;
//            te.selectPos = m_ItemName.Length;
//#else
//            te.cursorIndex = m_ItemName.Length;
//            te.selectIndex = m_ItemName.Length;
//#endif
//        }

//        #endregion


//        private static Texture2D GetIconForFile(string fileName)
//        {
//            var lastDot = fileName.LastIndexOf('.');
//            var extension = lastDot != -1 ? fileName.Substring(lastDot + 1).ToLower() : string.Empty;
//            switch (extension)
//            {
//                case "boo":
//                    return EditorGUIUtility.FindTexture("boo Script Icon");
//                case "cginc":
//                    return EditorGUIUtility.FindTexture("CGProgram Icon");
//                case "cs":
//                    return EditorGUIUtility.FindTexture("cs Script Icon");
//                case "guiskin":
//                    return EditorGUIUtility.FindTexture("GUISkin Icon");
//                case "js":
//                    return EditorGUIUtility.FindTexture("Js Script Icon");
//                case "mat":
//                    return EditorGUIUtility.FindTexture("Material Icon");
//                case "prefab":
//                    return EditorGUIUtility.FindTexture("PrefabNormal Icon");
//                case "shader":
//                    return EditorGUIUtility.FindTexture("Shader Icon");
//                case "txt":
//                    return EditorGUIUtility.FindTexture("TextAsset Icon");
//                case "unity":
//                    return EditorGUIUtility.FindTexture("SceneAsset Icon");
//                case "asset":
//                case "prefs":
//                    return EditorGUIUtility.FindTexture("GameManager Icon");
//                case "anim":
//                    return EditorGUIUtility.FindTexture("Animation Icon");
//                case "meta":
//                    return EditorGUIUtility.FindTexture("MetaFile Icon");
//                case "ttf":
//                case "otf":
//                case "fon":
//                case "fnt":
//                    return EditorGUIUtility.FindTexture("Font Icon");
//                case "aac":
//                case "aif":
//                case "aiff":
//                case "au":
//                case "mid":
//                case "midi":
//                case "mp3":
//                case "mpa":
//                case "ra":
//                case "ram":
//                case "wma":
//                case "wav":
//                case "wave":
//                case "ogg":
//                    return EditorGUIUtility.FindTexture("AudioClip Icon");
//                case "ai":
//                case "apng":
//                case "png":
//                case "bmp":
//                case "cdr":
//                case "dib":
//                case "eps":
//                case "exif":
//                case "gif":
//                case "ico":
//                case "icon":
//                case "j":
//                case "j2c":
//                case "j2k":
//                case "jas":
//                case "jiff":
//                case "jng":
//                case "jp2":
//                case "jpc":
//                case "jpe":
//                case "jpeg":
//                case "jpf":
//                case "jpg":
//                case "jpw":
//                case "jpx":
//                case "jtf":
//                case "mac":
//                case "omf":
//                case "qif":
//                case "qti":
//                case "qtif":
//                case "tex":
//                case "tfw":
//                case "tga":
//                case "tif":
//                case "tiff":
//                case "wmf":
//                case "psd":
//                case "exr":
//                    return EditorGUIUtility.FindTexture("Texture Icon");
//                case "3df":
//                case "3dm":
//                case "3dmf":
//                case "3ds":
//                case "3dv":
//                case "3dx":
//                case "blend":
//                case "c4d":
//                case "lwo":
//                case "lws":
//                case "ma":
//                case "max":
//                case "mb":
//                case "mesh":
//                case "obj":
//                case "vrl":
//                case "wrl":
//                case "wrz":
//                case "fbx":
//                    return EditorGUIUtility.FindTexture("Mesh Icon");
//                case "asf":
//                case "asx":
//                case "avi":
//                case "dat":
//                case "divx":
//                case "dvx":
//                case "mlv":
//                case "m2l":
//                case "m2t":
//                case "m2ts":
//                case "m2v":
//                case "m4e":
//                case "m4v":
//                case "mjp":
//                case "mov":
//                case "movie":
//                case "mp21":
//                case "mp4":
//                case "mpe":
//                case "mpeg":
//                case "mpg":
//                case "mpv2":
//                case "ogm":
//                case "qt":
//                case "rm":
//                case "rmvb":
//                case "wmw":
//                case "xvid":
//                    return EditorGUIUtility.FindTexture("MovieTexture Icon");
//                case "colors":
//                case "gradients":
//                case "curves":
//                case "curvesnormalized":
//                case "particlecurves":
//                case "particlecurvessigned":
//                case "particledoublecurves":
//                case "particledoublecurvessigned":
//                    return EditorGUIUtility.FindTexture("ScriptableObject Icon");
//            }
//            return EditorGUIUtility.FindTexture("DefaultAsset Icon");
//        }
//    }
//}