using System.Text;
using Aiuk.Common.Base;
using Aiuk.Common.PoolCache;

namespace AuikUnityCommon
{
    /// <summary>
    /// Vhtml（视图对象结构描述文档）解析所需数据。
    /// </summary>
    public class AiukVhtmlParserData
    {
        public AiukVhtmlParserData()
        {
            m_RightReadSb = AiukCommonFactory.StringBuilderPool.Take();
            m_LeftReadSb = AiukCommonFactory.StringBuilderPool.Take();
        }

        ~AiukVhtmlParserData()
        {
            m_RightReadSb.Clear();
            m_LeftReadSb.Clear();
            AiukCommonFactory.StringBuilderPool.Restore(m_RightReadSb);
            AiukCommonFactory.StringBuilderPool.Restore(m_LeftReadSb);
        }

        /// <summary>
        /// 视图结构描述（Vhtml）源代码。
        /// </summary>
        public string Code { get; private set; }

        public AiukVhtmlParserData(string code)
        {
            Code = code;
        }

        /// <summary>
        /// 当前待解析的代码字符索引。
        /// </summary>
        private int m_CurrentCharIndex;

        /// <summary>
        /// 当前待解析的代码字符。
        /// </summary>
        public char CurrentChar
        {
            get { return Code[m_CurrentCharIndex]; }
        }

        /// <summary>
        /// 当前待解析的代码字符的下一个字符。
        /// </summary>
        public char NextChar
        {
            get
            {
                return Code[m_CurrentCharIndex++];
            }
        }

        /// <summary>
        /// 将视图结构描述（Vhtml）源代码前移一个字符。
        /// </summary>
        public void ToNext()
        {
            m_CurrentCharIndex++;
        }

        /// <summary>
        /// 是否所有字符都已扫描完毕。
        /// </summary>
        public bool IsReadAble
        {
            get { return m_CurrentCharIndex < Code.Length; }
        }

        #region 代码预读

        #region 正向预读

        private readonly StringBuilder m_RightReadSb;

        private int m_RightIndex;

        /// <summary>
        /// 正向读取指定长度的字符并返回读取的字符串。
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public string RightRead(int length)
        {
            var startIndex = Code.Length - m_LeftIndex;
            m_LeftIndex += length;
            var readStr = Code.Substring(startIndex, length);
            return readStr;
        }

        #endregion

        #region 反向预读

        private readonly StringBuilder m_LeftReadSb;

        private int m_LeftIndex;

        /// <summary>
        /// 反向读取指定长度的字符并返回读取的字符串。
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public string LeftRead(int length)
        {
            m_LeftIndex += length;
            var startIndex = Code.Length - m_LeftIndex;
            var readStr = Code.Substring(startIndex, length);
            return readStr;
        }

        #endregion


        #endregion


        #region Read Code String

        /// <summary>
        /// 用于读取代码字符串的字符串构建器。
        /// </summary>
        private StringBuilder m_ReadSb;

        /// <summary>
        /// 读取指定的长度并返回字符串。
        /// 如果没有指定读取的起始位置，则默认为当前位置。
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public string Read(int length, int startIndex = -1)
        {
            if (m_CurrentCharIndex + length >= Code.Length)
                throw new AiukVhtmlParseException("读取位置超出索引！");

            m_ReadSb.Clear();
            for (var i = 0; i < length; i++)
            {
                m_CurrentCharIndex++;
                m_ReadSb.Append(CurrentChar);
            }

            var result = m_ReadSb.ToString();
            return result;
        }




        #endregion



    }
}

