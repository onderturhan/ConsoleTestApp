using System;
using System.Threading;

namespace ConsoleTestApp7
{
    //public delegate void SpeedDelegate(int currentSpeed);
    class Car
    {
        public event Action<int> /*SpeedDelegate*/ speedEvent;
        private int _speed;

        public string Model { get; set; }
        public int Speed
        {
            get => _speed;
            set
            {
                if (value > 80 && speedEvent != null)
                {
                    speedEvent(value); //eventin fırlatıldığı an
                }
                else
                {
                    _speed = value;
                }
            }
        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            Car c = new Car();
            c.Model = "Opel Corsa";
            ////c.speedEvent += C_speedEvent;
            c.speedEvent += (speedValue) => { Console.WriteLine("Car is extended of speed, current: " + speedValue); };
            for (int i = 60; i < 100; i+=5)
            {
                c.Speed = i;
                Console.WriteLine("Anlık Hız:" + i);
                Thread.Sleep(1000);
            }
        }

        ////private static void C_speedEvent(int currentSpeed)
        ////{
        ////    Console.WriteLine("Car is extended of speed, current: " + currentSpeed);
        ////}
    }
}
