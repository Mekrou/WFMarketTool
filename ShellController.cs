using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFMarketTool
{
    public class ShellController
    {
        public ShellController()
        {
        }

        public void Activate()
        {
            do
            {
                Shell.Display();
                Shell.Read();
            } while (!(Shell.isValidCommand()));
        }
    }
}
