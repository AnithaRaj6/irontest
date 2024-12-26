using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coding.Challange.IsankaThalagala.Utility
{
    public static class ButtonMapper
    {
        // button mapping > numbers to characters
        public static readonly Dictionary<string, string> ButtonMapping = new Dictionary<string, string>
        {
            {"1", "&'(" },
            {"2", "ABC" },
            {"3", "DEF" },
            {"4", "GHI" },
            {"5", "JKL" },
            {"6", "MNO" },
            {"7", "PQRS" },
            {"8", "TUV" },
            {"9", "WXYZ" },
            {"0", " " },
        };
    }
}
