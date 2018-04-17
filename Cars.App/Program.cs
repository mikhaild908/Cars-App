using System;
using System.Collections.Generic;
using Cars.Library;

namespace Cars.App
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Car> cars = new List<Car>();

            AddCars(cars);
            DisplayCarDetails(cars);

            Console.ReadLine();
        }

        private static void DisplayCarDetails(List<Car> cars)
        {
            foreach (var car in cars)
            {
                Console.WriteLine("Name: " + car.Name);
                Console.WriteLine("Make: " + car.Make);
                Console.WriteLine("Year: " + car.Year);

                int hours = 5;
                Console.WriteLine($"{car.Name} travelled for {hours} hours.");
                car.Move(hours);
                
                Console.WriteLine($"Ave Speed: {car.Speed} mph");

                Console.WriteLine("Distance Travelled: " + car.DistanceTravelled + " miles");

                for (int i = 0; i < car.DistanceTravelled; i = i + 10)
                {
                    Console.Write("=");
                }

                Console.WriteLine();
                Console.WriteLine();
            }
        }

        private static void AddCars(List<Car> cars)
        {
            Car c1 = new Civic("My Civic", 2016);
            Car c2 = new Corolla("My Corolla", 2016);
            Car c3 = new ModelS("My Model S", 2016);

            cars.Add(c1);
            cars.Add(c2);
            cars.Add(c3);
        }
    }
}