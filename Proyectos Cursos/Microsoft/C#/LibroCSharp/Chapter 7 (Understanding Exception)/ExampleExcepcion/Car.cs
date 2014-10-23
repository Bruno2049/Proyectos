using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleExcepcion
{
    public class Car
    {
        public const int MaxSpeed = 100;
        // Car properties.
        public int CurrentSpeed { get; set; }
        public string PetName { get; set; }
        // Is the car still operational?
        private bool carIsDead;
        // A car has-a radio.
        private Radio theMusicBox = new Radio();
        // Constructors.
        public Car() { }

        public Car(string name, int speed)
        {
            CurrentSpeed = speed;
            PetName = name;
        }
        public void CrankTunes(bool state)
        {
            // Delegate request to inner object.
            theMusicBox.TurnOn(state);
        }
        // See if Car has overheated.
        public void Accelerate(int delta)
        {
            if (carIsDead)
                Console.WriteLine("{0} is out of order...", PetName);
            else
            {
                CurrentSpeed += delta;
                if (CurrentSpeed > MaxSpeed)
                {
                    Console.WriteLine("{0} has overheated!", PetName);
                    CurrentSpeed = 0;
                    
                    throw new Exception(string.Format("{0} has overheated!", PetName));
                    carIsDead = true;
                }
                else
                    Console.WriteLine("=> CurrentSpeed = {0}", CurrentSpeed);
            }
        }
    }
}