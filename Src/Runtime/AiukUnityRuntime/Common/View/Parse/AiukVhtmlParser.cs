using System.Text;

namespace AuikUnityCommon
{
    public class AiukVhtmlParser
    {
        #region Field

        /// <summary>
        /// 
        /// </summary>
        private AiukVhtmlParserData m_ParserData;

        #endregion

        #region Public Method

        /// <summary>
        /// 解析给定的Vxml代码并返回创建的游戏对象。
        /// </summary>
        /// <param name="parserData">视图结构解析器所需使用的数据。</param>
        /// <returns></returns>
        public void ParseVxmlDataCode(AiukVhtmlParserData parserData)
        {
            m_ParserData = parserData;
            Scan();
        }

        #endregion

        #region Scan

        private void Scan()
        {
            while (m_ParserData.IsReadAble)
            {
                var c = m_ParserData.CurrentChar;

                switch (c)
                {
                    case '<':
                        m_ParserData.ToNext();
                        ScanStartTag();
                        break;
                    case 'n':
                        ScanNameIdentifier();
                        break;
                    case '=':
                        if (m_ParserData.NextChar != '\"')
                            throw new AiukVhtmlParseException("标识符属性赋值需要一个双引号！");

                        m_ParserData.ToNext();
                        m_ParserData.ToNext();
                        ReadIdentifierValue();
                        break;
                }
            }
        }

        #region TagScan


        private void ScanStartTag()
        {
            var c = m_ParserData.CurrentChar;

            switch (c)
            {
                case 'D':
                case 'd':
                    ScanDivTag();
                    break;
                case 'P':
                case 'p':
                    ScanPTag();
                    break;
                case 'S':
                case 's':
                    ScanSpanTag();
                    break;
            }
        }
        private void ScanPTag()
        {

        }

        private void ScanSpanTag()
        {

        }

        private void ScanDivTag()
        {
            var readStr = m_ParserData.Read(2);
            if (readStr.ToLower() == AiukVhtmlTagConstant.DIV_TAG)  //  Div标签
            {
//                var token = new AiukVhtmlToken { Tag = AiukVhtmlTag.Div };
            }
            //  自定义标签
            else
            {

            }
        }

        #endregion

        #region IdentifierScan

        private void ScanNameIdentifier()
        {
            var readStr = m_ParserData.Read(3);
            if (readStr == AiukVhtmlIdentifierConstant.NAME_IDENTIFIER)
            {
//                var token = new AiukVhtmlToken
//                {
//                    Identifier = AiukVhtmlTokenIdentifier.Name,
//                };
            }
        }

        #endregion

        #region SancIdentifierValue

        private void ReadIdentifierValue()
        {
            // todo 使用对象池获取一个字符串构建器实例。
            var sb = new StringBuilder();

            while (true)
            {
                var c = m_ParserData.CurrentChar;
                if (c == '\"')
                {
                    var value = sb.ToString();
                    var token = new AiukVhtmlToken
                    {
                        Literals = value,
                    };
                    return;
                }

                sb.Append(c);
                ReadIdentifierValue();
            }
        }

        #endregion

        #endregion







    }
}


