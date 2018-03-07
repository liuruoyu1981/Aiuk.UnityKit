using Aiuk.Common;
using Aiuk.Common.Base;
using Aiuk.Common.Utility;
using UnityEditor;
using UnityEngine;

namespace AiukUnityEditor
{
    /// <summary>
    /// Cs脚本创建窗口
    /// 用于基本的csharp脚本创建
    /// </summary>
    public class CsCreateWindow : EditorWindow
    {
        #region 字段

        private static CsCreateWindow _Window;
        private static readonly Vector2 FixedSize = new Vector2(680, 520);
        private AiukEditorLayouter m_EditorLayouter;
        private AiukStringAppender m_Appender;
        private string m_NameSpace = "AiukUnityRuntime";
        private string m_ScriptName;
        private readonly string[] m_ScriptTypeArray = { "class", "interface", "enum" };
        private int m_ScriptTypeIndex;
        private string ScriptTypeStr
        {
            get
            {
                var value = m_ScriptTypeArray[m_ScriptTypeIndex];
                return value;
            }
        }
        private string m_TargetDir;
//        private string m_WarningDisable = "3001,3002,3003,3009,4014";
        private string m_DeveloperName;
        private string m_DeveloperEmail;
        private string m_ScriptContentReview;

        #endregion

        public static void ShowWindow(string targetDir)
        {
            if (_Window != null)
            {
                _Window.Focus();
                return;
            }

            _Window = GetWindow<CsCreateWindow>();
            _Window.titleContent = new GUIContent("CS创建器");
            _Window.Init(targetDir);
        }

        private void Init(string targetDir)
        {
            m_TargetDir = targetDir;
            minSize = FixedSize;
            maxSize = FixedSize;
            m_EditorLayouter = new AiukEditorLayouter(10, 10);
            m_Appender = new AiukStringAppender();
            UpdateScriptContent();
        }

        private void OnGUI()
        {
            m_EditorLayouter.Reset();
            GUILayout.Space(10);
            m_EditorLayouter.LabelField(150, 20, "NameSpace");
            m_EditorLayouter.TextArea(200, 20, m_NameSpace, null, s =>
            {
                m_NameSpace = s;
                UpdateScriptContent();
            });
            m_EditorLayouter.NextLine();

            m_EditorLayouter.LabelField(150, 20, "ScriptName");
            m_EditorLayouter.TextArea(200, 20, m_ScriptName, null, s =>
            {
                m_ScriptName = s;
                UpdateScriptContent();
            });
            m_EditorLayouter.NextLine();

            m_EditorLayouter.LabelField(150, 20, "ScriptType");
            m_ScriptTypeIndex = m_EditorLayouter.Popup(200, 20, m_ScriptTypeIndex, m_ScriptTypeArray);
            m_EditorLayouter.NextLine();

//            m_EditorLayouter.LabelField(150, 20, "WarningDisable");
//            m_WarningDisable = m_EditorLayouter.TextArea(200, 20, m_WarningDisable);
//            m_EditorLayouter.NextLine();

            m_EditorLayouter.LabelField(150, 20, "DeveloperName");
            m_DeveloperName = m_EditorLayouter.TextArea(200, 20, m_DeveloperName);
            m_EditorLayouter.NextLine();

            m_EditorLayouter.LabelField(150, 20, "DeveloperEmail");
            m_DeveloperEmail = m_EditorLayouter.TextArea(200, 20, m_DeveloperEmail);
            m_EditorLayouter.NextLine();

            m_EditorLayouter.SetStartPoint(10, 490);
            m_EditorLayouter.Button(100, 20, "CreateScript", CreateScript);
            m_EditorLayouter.Button(100, 20, "ReviewCode", UpdateScriptContent);

            m_EditorLayouter.SetStartPoint(370, 10);
            m_ScriptContentReview = m_EditorLayouter.TextArea(300, 500, m_ScriptContentReview);
        }

        private void OnDestroy()
        {
            _Window = null;
        }

        internal enum ScriptType : byte
        {
            Class,
            Interface,
            Enum,
        }

        #region 脚本创建

        private void UpdateScriptContent()
        {
//            AppendWaringDisable();  // 预编译头
            AppendNoteHead();   // 注释头
            AppendTypeCode();   //  代码体

            m_ScriptContentReview = m_Appender.ToString();
            m_Appender.Clean();
        }
//
//        private void AppendWaringDisable()
//        {
//            var array = m_WarningDisable.Split(',');
//            foreach (var id in array)
//            {
//                m_Appender.AppendLine("#pragma warning disable CS{0}", id);
//            }
//
//            m_Appender.AppendLine();
//        }

        private void AppendNoteHead()
        {
            m_Appender.AppendLine("#region Head");
            m_Appender.AppendLine();
            m_Appender.AppendLine("// Author:        liuruoyu1981");
            m_Appender.AppendLine("// CreateDate:    {0}", AiukDateUtility.DateAndTimeForNow);
            m_Appender.AppendLine("// Email:         liuruoyu1981@gmail.com || 35490136@qq.com");
            m_Appender.AppendLine();
            m_Appender.AppendLine("#endregion");
            m_Appender.AppendLine();
        }

        private void AppendTypeCode()
        {
            m_Appender.AppendLine("namespace " + m_NameSpace);
            m_Appender.AppendLine("{");
            m_Appender.ToRight();

            m_Appender.AppendLine("public {0} {1}", ScriptTypeStr, m_ScriptName);
            m_Appender.AppendLine("{");
            m_Appender.AppendLine();
            m_Appender.AppendLine("}");

            m_Appender.ToLeft();
            m_Appender.AppendLine("}");
        }

        private void CreateScript()
        {
            if (string.IsNullOrEmpty(m_ScriptName))
            {
                EditorUtility.DisplayDialog("错误", "脚本名不能为空！", "知道了");
                return;
            }

            if (!m_TargetDir.EndsWith("/", System.StringComparison.Ordinal))
            {
                m_TargetDir += "/";
            }

            var targetPath = m_TargetDir + m_ScriptName + ".cs";
            AiukIOUtility.WriteAllText(targetPath, m_ScriptContentReview);
            AssetDatabase.Refresh();
            _Window.Close();
        }

        #endregion
    }



}

