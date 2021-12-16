using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDelegate
{

    // This class contains methods BinaryOp will point to.
    public class SimpleMath
    {
        public int Add(int x, int y) => x + y;
        public static int Subtract(int x, int y) => x - y;
        public static int SquareNumber(int a) => a * a;
        public static void DisplayDelegateInfo(Delegate delObj)
        {
            // Print the names of each member in the
            // delegate's invocation list.
            foreach (Delegate d in delObj.GetInvocationList())
            {
                Console.WriteLine("Method Name: {0}", d.Method);
                Console.WriteLine("Type Name: {0}", d.Target);
            }
        }
    }
}
