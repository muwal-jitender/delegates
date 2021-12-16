using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CarDelegate
{
    /// <summary>
    /// Car class that can inform external entities about its current engine state
    /// </summary>
    public class Car
    {
        // Step: 1 - Define a new delegate type that will be used to send notifications to the caller.
        public delegate void CarEngineHandler(string msgForCaller);
        // Step: 2 - Declare a member variable of this delegate
        private CarEngineHandler _ListOfHandlers;
        // Step: 3 - Create a helper function on the Car that allows the caller to specify the method to call back on
        public void RegisterWithCarEngine(CarEngineHandler methodToCall)
        {
            // add multiple methods to a delegate object, you simply use the overloaded += operator
            _ListOfHandlers += methodToCall;
        }

        public void UnRegisterWithCarEngine(CarEngineHandler methodToCall)
        {
            // Remove methods to a delegate object, you simply use the overloaded -= operator
            _ListOfHandlers -= methodToCall;
        }

        // Step: 4 - Implement the Accelerate() method to invoke the delegate's invocation list under the correct circumstances.
        public void Accelerate(int delta)
        {
            // If the car is "dead", send dead message;
            if (_carIsDead)
            {
                // The reason "null propagation syntax" is that If the caller does not call this RegisterWithCarEngine
                // method and you attempt to use the Car class, you will get a NullReferenceException at runtime.
                _ListOfHandlers?.Invoke("Sorry, this car id dead...");
                return;
            }
            CurrentSpeed += delta;
            if (10 == (MaxSpeed - CurrentSpeed))
            {
                _ListOfHandlers?.Invoke("Careful buddy! Gonna blow!");
            }
            if (CurrentSpeed > MaxSpeed)
            {
                _carIsDead = true;
            }
            else
            {
                Console.WriteLine("CurrentSpeed = {0}", CurrentSpeed);
            }
        }

        // Internal state data.
        public int CurrentSpeed { get; set; }
        public int MaxSpeed { get; set; } = 100;
        public string PetName { get; set; }
        // Is the car alive or dead?
        private bool _carIsDead;
        // Class constructors.
        public Car() { }
        public Car(string name, int maxSp, int currSp)
        {
            CurrentSpeed = currSp;
            MaxSpeed = maxSp;
            PetName = name;
        }
    }

    public class AccessCar
    {
        public void CallMembers()
        {
            Car c1 = new Car("Ford", 100, 10);
            // Now, tell the car which method to call when it wants to send us messages.
            //c1.RegisterWithCarEngine(new Car.CarEngineHandler(OnCarEngineEvent));
            c1.RegisterWithCarEngine(new Car.CarEngineHandler(OnCarEngineEvent));
            // Using method group conversion syntex to register a method
            // Notice that you are not directly allocating the associated delegate object but rather simply specifying a method that matches the delegate’s expected signature
            // Understand that the C# compiler is still ensuring type safety, means error if method does not match the deleate signature
            c1.RegisterWithCarEngine(OnCarEngineEvent2);
            c1.UnRegisterWithCarEngine(new Car.CarEngineHandler(OnCarEngineEvent2));
            // Speed up (this will trigger the events).
            Console.WriteLine("***** Speeding up *****");
            for (int i = 0; i < 6; i++)
            {
                c1.Accelerate(20);
            }
        }
        static void OnCarEngineEvent(string msg)
        {
            Console.WriteLine("\n*** Message From Car Object ***");
            Console.WriteLine("=> {0}", msg);
            Console.WriteLine("********************\n");
        }

        /// <summary>
        /// For the purpose of Multicast delegate
        /// </summary>
        /// <param name="msg"></param>
        static void OnCarEngineEvent2(string msg)
        {
            Console.WriteLine("=> {0}", msg.ToUpper());
        }
    }
}
