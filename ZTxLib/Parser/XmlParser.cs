namespace ZTxLib.Parser
{
    public class XmlParser
    {
        private readonly string str;
        public XmlParser(string str)
        {
            this.str = str;
        }
        public override string ToString()
        {
            return str;
        }
    }
}