using System.Collections.Generic;

namespace Stop_Phishing.Helpers.Token
{
    public class ActionResponse
    {
        public string Token { get; set; }
        public bool Result { get; set; }
        public IEnumerable<string> Messages { get; set; }
    }
}