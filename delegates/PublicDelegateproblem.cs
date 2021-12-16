using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegates.PublicDelegateproblem
{
    public class Car
    {
        public delegate void CarEngineHandler(string msgForCaller);

        public CarEngineHandler ListOfHandlers;
        public void Accelerate(int delta)
        {
            if (ListOfHandlers != null)
            {
                ListOfHandlers("This car is dead...");
            }
        }
    }
    public class AccessCar
    {
        public void CallMembers()
        {
            Car myCar = new();
            myCar.ListOfHandlers = (msg) => Console.WriteLine(msg);
            myCar.ListOfHandlers.Invoke("hee, hee, hee, I can also directly invoke the delegate.");
        }
    }
    /*Exposing public delegate members breaks encapsulation, which not only can lead to code that is hard
      to maintain (and debug) but could also open your application to possible security risks! */
}
