using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IConversion
    {
        Dictionary<string, int> GetTurkishNumberMappings();
        public string CleanExcessiveSpaces(string input);
        public string ReplaceTurkishWordsToNumber(string request);
        public string TransformInput(string request);
        
    }
}
