using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFMarketTool
{
    public class ShellController
    {
        public Shell shell;

        public ShellController()
        {
            shell = new Shell();
        }

        public void Activate()
        {
            do
            {
                shell.Display();
                shell.Read();

                Shell._feedbackMessages.Add($"Result of shell.isValidCommand(): {shell.isValidCommand()}");
            } while (!(shell.isValidCommand()));
        }
    }
}
