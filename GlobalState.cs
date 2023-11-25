using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFMarketTool
{
    internal class GlobalState
    {
        public static bool Verbose { get; set; }

        static GlobalState()
        {
            Verbose = true;
        }
    }
}

namespace DotPrompt
{

}
