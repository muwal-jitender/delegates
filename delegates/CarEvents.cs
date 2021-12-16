using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegates.CarEvents
{
    /// <summary>
    /// Car class that can inform external entities about its current engine state
    /// </summary>
    public class Car
    {
        // Step: 1 - Define a new delegate type that will be used to send notifications to the caller.
        //public delegate void CarEngineHandler(string msgForCaller);

        // Creating Custom Event Arguments
        public delegate void CarEngineHandler(object sender, CarEventArgs e);
        // Step: 2 - This car can send these events.
        public event CarEngineHandler Exploded;
        public event CarEngineHandler AboutToBlow;

        // Step: 3 - Implement the Accelerate() method to invoke the delegate's invocation list under the correct circumstances.
        public void Accelerate(int delta)
        {
            // If the car is "dead", send dead message;
            if (_carIsDead)
            {
                // The reason "null propagation syntax" is that If the caller does not call this RegisterWithCarEngine
                // method and you attempt to use the Car class, you will get a NullReferenceException at runtime.
                Exploded?.Invoke(this, new CarEventArgs("Sorry, this car id dead..."));
                return;
            }
            CurrentSpeed += delta;
            if (10 == (MaxSpeed - CurrentSpeed))
            {
                AboutToBlow?.Invoke(this, new CarEventArgs("Careful buddy! Gonna blow!"));
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
    public class CarEventArgs : EventArgs
    {
        public readonly string msg;
        public CarEventArgs(string message)
        {
            msg = message;
        }
    }
    public class AccessCar
    {
        public static void CallMembers()
        {
            Car c1 = new Car("SlugBug", 100, 10);
            c1.AboutToBlow += CarAboutToBlow;
            c1.Exploded += CarIsAlmostDoomed;


            Console.WriteLine("***** Speeding up *****");
            for (int i = 0; i < 6; i++)
            {
                c1.Accelerate(20);
            }

            // Remove CarExploded method
            // from invocation list.
            c1.Exploded -= CarExploded;
            Console.WriteLine("\n***** Speeding up *****");
            for (int i = 0; i < 6; i++)
            {
                c1.Accelerate(20);
            }
        }



        private static void CarAboutToBlow(object sender, CarEventArgs e)
        {
            if (sender is Car c)
            {
                Console.WriteLine($"{c.PetName} says: {e.msg}");
            }
        }
        private static void CarIsAlmostDoomed(object sender, CarEventArgs e)
        {
            if (sender is Car c)
            {
                Console.WriteLine($"=> Critical Message from {c.PetName}: {e.msg}");
            }
        }

        private static void CarExploded(object sender, CarEventArgs e)
        {
            Console.WriteLine(e.msg);
        }
    }
}
