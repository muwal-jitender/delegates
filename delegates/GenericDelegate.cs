using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegates.generic
{
    public class GenericDelegate
    {
        // This generic delegate can represent any method
        // returning void and taking a single parameter of type T.
        public delegate void MyGenericDelegate<in T>(T arg);

        public static void CallMembers()
        {
            Console.WriteLine("***** Generic Delegates *****\n");
            MyGenericDelegate<string> stringTarget = new (strTarget);
            stringTarget("Jitender");

            MyGenericDelegate<int> intTarget = new(IntTarget);
            intTarget(9);
        }

        public static void strTarget(string arg)
        {
            Console.WriteLine("arg in uppercase is: {0}", arg.ToUpper());
        }
        static void IntTarget(int arg)
        {
            Console.WriteLine("++arg is: {0}", ++arg);
        }
    }
}
