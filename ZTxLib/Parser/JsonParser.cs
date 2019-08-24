using System.Collections.Generic;

namespace ZTxLib.Parser
{
    public class JsonParser
    {
        //private class Tag
        //{
        //    private readonly bool IsValue;
        //    private readonly string value;
        //    private readonly Tag arr;
        //    public Tag()
        //    {
        //    }
        //}
        private readonly string Text;
        private readonly Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
        public JsonParser(string JSON_Text)
        {
            Text = JSON_Text;
            string[] vs = Text.Split('"');
            if (vs.Length % 4 != 1) return;
            for (int i = 3; i < vs.Length; i += 4)
                keyValuePairs.Add(vs[i - 2], vs[i]);
        }
        public string this[string key] => keyValuePairs[key];
        public override string ToString()
        {
            return Text;
        }
    }
}       
