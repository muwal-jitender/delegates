using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegates.ActionAndFuncDelegates
{
    public class ActionDelegate
    {
        public static void DisplayMessage(string msg, ConsoleColor txtColor, int printCount)
        {
            ConsoleColor previous = Console.ForegroundColor;
            Console.ForegroundColor = txtColor;

            for (int i = 0; i < printCount; i++)
            {
                Console.WriteLine(msg);
            }

            Console.ForegroundColor = previous;
        }
        public static void CallMembers()
        {
            // Now, rather than building a custom delegate manually to pass the program’s flow to the
            // DisplayMessage() method, you can use the out-of-the-box Action <> delegate, as shown below
            Action<string, ConsoleColor, int> action = DisplayMessage;
            action("Action Message!", ConsoleColor.Yellow, 5);
        }
    }
}
