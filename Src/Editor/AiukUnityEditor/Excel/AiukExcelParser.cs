using System.Collections.Generic;
using System.Data;
using System.IO;
using Aiuk.Common.Base;
using Aiuk.Common.Utility;
using Excel;

namespace AiukUnityEditor
{
    /// <summary>
    /// excel解析器。
    /// </summary>
    public class AiukExcelParser
    {
        #region 字段

        /// <summary>
        /// 单元格字段列表。
        /// </summary>
        private readonly List<AiukExcelFieldInfo> m_FieldInfos = new List<AiukExcelFieldInfo>();

        /// <summary>
        /// 分隔符字符串数组。
        /// </summary>
        private readonly string[] m_Separator = { "[__]" };

        /// <summary>
        /// 目标Excel单元集合。
        /// </summary>
        private readonly DataTableCollection m_Tables;

        /// <summary>
        /// 首页单元表。
        /// </summary>
        private readonly DataTable m_FirstTable;

        /// <summary>
        /// 输出脚本时的类名。
        /// </summary>
        private string m_ClassName = "DefalutExcalTable";

        /// <summary>
        /// 脚本输出字符串构造器。
        /// </summary>
        private readonly AiukStringAppender m_Appender;

        /// <summary>
        /// 脚本创建者姓名。
        /// </summary>
        private readonly string m_CoderName;

        /// <summary>
        /// 脚本创建者邮箱。
        /// </summary>
        private readonly string m_CodeEmail;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造一个Excel解析器实例。
        /// </summary>
        /// <param name="path">excel文件路径。</param>
        /// <param name="className">输出脚本时的类名。</param>
        /// <param name="codeName">脚本创建者姓名。</param>
        /// <param name="codeEmail">脚本创建者邮箱。</param>
        public AiukExcelParser(string path, string className,
            string codeName = null, string codeEmail = null)
        {
            using (var fs = File.Open(path, FileMode.Open, FileAccess.ReadWrite))
            {
                var reader = ExcelReaderFactory.CreateOpenXmlReader(fs);
                var dataSet = reader.AsDataSet();
                m_FirstTable = dataSet.Tables[0];
                m_Tables = dataSet.Tables;
            }

            m_Appender = new AiukStringAppender();
            m_ClassName = className;
            ParseFieldInfo();
        }

        #endregion

        #region 私有函数

        /// <summary>
        /// 解析目标表的字段信息。
        /// </summary>
        private void ParseFieldInfo()
        {
            var first = m_FirstTable.Rows[0];
            var second = m_FirstTable.Rows[1];
            var length = m_FirstTable.Columns.Count;
            for (var i = 0; i < length; i++)
            {
                var filedInfo = new AiukExcelFieldInfo();
                var typeAndNameStr = first[i].ToString();
                var comment = second[i].ToString();
                filedInfo.SetTypeAndName(typeAndNameStr)
                    .SetComment(comment)
                    .SetIndex(i);

                m_FieldInfos.Add(filedInfo);
            }
        }

        #endregion

        #region Csharp

        /// <summary>
        /// 导出Cs脚本。
        /// </summary>
        public void ExportCsScript(string path)
        {
            AppendCsHeader();
            AppendCsField();
            AppendCsCreateEntityMethod();
            AppendCsCreateEntitysMethod();
            AppendCsToTxtMethod();
            AppendCsDeepCopyMethod();
            AiukIOUtility.WriteAllText(path, m_Appender.ToString());
        }

        private void AppendCsHeader()
        {
            m_Appender.AppendCsComment("Excel数据表_" + m_ClassName);
            m_Appender.AppendLine("[Serializable]");
            m_Appender.AppendLine("public class {0} : IDeepCopyLocalData<{0}>", m_ClassName);
        }

        private void AppendCsField()
        {
            m_FieldInfos.ForEach(field =>
            {
                m_Appender.AppendCsComment(field.Comment);
                m_Appender.AppendLine(string.Format("public {0} {1};",
                    field.CsharpOriginType, field.Name));
                m_Appender.AppendLine();
            });
        }

        private void AppendCsCreateEntityMethod()
        {
            m_Appender.AppendLine(string.Format("public {0} CreateEntity(List<string> row)", m_ClassName));
            m_Appender.AppendLine("{");
            m_Appender.AppendLine(string.Format("{0} entity = new {0}();", m_ClassName));

            m_FieldInfos.ForEach(info =>
            {
                m_Appender.AppendLine(string.Format("{0}", info.CsharpEntityStr));
            });

            m_Appender.AppendLine("return entity;");
            m_Appender.AppendLine("}");
            m_Appender.AppendLine();
        }

        private void AppendCsCreateEntitysMethod()
        {
            m_Appender.AppendLine(string.Format("public List<{0}> CreateEntitys(List<string> listObj)",
                m_ClassName));
            m_Appender.AppendLine("{");
            m_Appender.AppendLine(string.Format("var result = new List<{0}>();", m_ClassName));
            m_Appender.AppendLine("foreach (var list in listObj)");
            m_Appender.AppendLine("{");
            m_Appender.AppendLine("var entityListText = list.Split(Constant.TxtSeparators, StringSplitOptions.None).ToList();");
            m_Appender.AppendLine("var entity = CreateEntity(entityListText);");
            m_Appender.AppendLine("result.Add(entity);");
            m_Appender.AppendLine("}");
            m_Appender.AppendLine("return result;");
            m_Appender.AppendLine("}");
            m_Appender.AppendLine();
        }

        private void AppendCsToTxtMethod()
        {
            m_Appender.AppendCsComment("将本地数据对象转换为txt源数据字符串");
            m_Appender.AppendLine("public string ToTxt()");
            m_Appender.AppendLine("{");
            m_Appender.AppendLine("string entityStr = string.Empty;");

            m_FieldInfos.ForEach(info =>
            {
                m_Appender.AppendLine(string.Format("entityStr = entityStr + {0} + ", info.Name)
                                      + "\"" + m_Separator + "\"" + ";");
            });

            m_Appender.AppendLine("entityStr = entityStr.Remove(entityStr.Length - 1);");
            m_Appender.AppendLine("return entityStr;");
            m_Appender.AppendLine("}");
            m_Appender.AppendLine();
        }

        private void AppendCsDeepCopyMethod()
        {
            m_Appender.AppendCsComment("获得本地数据的一份深度复制副本");
            m_Appender.AppendLine(string.Format("public {0} DeepCopy()", m_ClassName));
            m_Appender.AppendLine("{");
            m_Appender.AppendLine("var buff = SerializeUitlity.Serialize(this);");
            m_Appender.AppendLine(string.Format("var entity = SerializeUitlity.DeSerialize<{0}>(buff);",
                m_ClassName));
            m_Appender.AppendLine("return entity;");
            m_Appender.AppendLine("}");
            m_Appender.AppendLine("}");
            m_Appender.AppendLine("}");
        }

        #endregion

        #region 内部类-Excel字段信息

        private class AiukExcelFieldInfo
        {
            public string Name;
            private string OriginType;
            public string Comment;

            public string TypeScriptType
            {
                get
                {
                    switch (OriginType)
                    {
                        case "int":
                            return "number";
                        case "string":
                            return "string";
                        case "List<int>":
                            return "int[]";
                        case "List<float>":
                            return "number[]";
                        case "bool":
                            return "boolean";
                        case "float":
                            return "number";
                        case "List<string>":
                            return "string[]";
                        default:
                            return "";
                    }
                }
            }

            public string CsharpOriginType { get { return OriginType; } }
            //            public string LuaType;
            private int Index;

            public string CsharpEntityStr
            {
                get
                {
                    switch (OriginType)
                    {
                        case "int":
                            return string.Format("entity.{0} = Convert.ToInt32(row[{1}]);", Name, Index);
                        case "string":
                            return string.Format("entity.{0} = row[{1}];", Name, Index);
                        case "List<int>":
                            return string.Format("entity.{0} = row[{1}].ToListInt();", Name, Index);
                        case "List<float>":
                            return string.Format("entity.{0} = row[{1}.ToListFloat(););", Name, Index);
                        case "bool":
                            return string.Format("entity.{0} = Convert.ToBoolean(row[{1}]);", Name, Index);
                        case "float":
                            return string.Format("entity.{0} = float.Parse(row[{1}]);", Name, Index);
                        case "List<string>":
                            return string.Format("entity.{0} = row[{1}].Split(',').ToList();", Name, Index);
                        default:
                            return "";
                    }
                }
            }

            public string TypeScriptEntityStr
            {
                get
                {
                    switch (OriginType)
                    {
                        case "int":
                            return string.Format("entity.{0} = parseInt(row[{1}]);", Name, Index);
                        case "string":
                            return string.Format("entity.{0} = row[{1}];", Name, Index);
                        case "List<int>":
                            return string.Format("entity.{0} = parseListInt(row[{1}]);", Name, Index);
                        case "List<float>":
                            return string.Format("entity.{0} = parseListFloat(row[{1}]);", Name, Index);
                        case "bool":
                            return string.Format("entity.{0} = parseBoolean(row[{1}]);", Name, Index);
                        case "float":
                            return string.Format("entity.{0} = parseFloat(row[{1}]);", Name, Index);
                        case "List<string>":
                            return string.Format("entity.{0} = row[{1}].split('_');", Name, Index);
                        default:
                            return "";
                    }
                }
            }

            /// <summary>
            /// 以约定的_拆分字段单元格名得到字段类型及程序字段名。
            /// </summary>
            /// <param name="typeAndName">字段单元格中的值。</param>
            /// <returns></returns>
            public AiukExcelFieldInfo SetTypeAndName(string typeAndName)
            {
                var firstArray = typeAndName.Split('_');
                Name = firstArray[1];
                OriginType = firstArray[0];
                return this;
            }

            /// <summary>
            /// 设置字段注释。
            /// </summary>
            /// <param name="comment">字段注释。</param>
            /// <returns></returns>
            public AiukExcelFieldInfo SetComment(string comment)
            {
                Comment = comment;
                return this;
            }

            /// <summary>
            /// 设置字段索引。
            /// </summary>
            /// <param name="index">字段索引。</param>
            /// <returns></returns>
            public AiukExcelFieldInfo SetIndex(int index)
            {
                Index = index;
                return this;
            }


        }

        #endregion

    }
}
