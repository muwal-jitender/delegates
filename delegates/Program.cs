using System;
using CarDelegate;
using delegates.generic;
using delegates.ActionAndFuncDelegates;
namespace delegates
{
    class Program
    {        
        static void Main(string[] args)
        {

            delegates.CarEvents.AccessCar.CallMembers();            
            Console.ReadLine();
        }
    }
}
