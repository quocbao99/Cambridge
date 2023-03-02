using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class GenerateCodeUtilities
    {
        public static string GenerateCode(string type, string name) {
            Random random = new Random();
            var numRandom= random.NextDouble() * (1000000 - 1) + 1;
            double code = Convert.ToInt64(Timestamp.Now() + numRandom);
            return type.Replace(" ","").ToUpper() + "-" + AppUtilities.RemoveUnicode(name.Replace(" ", "")).ToUpper() + "-" + code.ToString();
        }
    }
}
