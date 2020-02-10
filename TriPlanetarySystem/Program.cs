using System;
using System.Linq;
using System.Text;
using Library;

namespace TriPlanetarySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Execute();
        }

        private static void Execute()
        {
            var system = InitializeSystem();

            Console.WriteLine("\n ------------------------------------------- \n");
            Console.WriteLine("Predicción del clima para los próximos 10 años \n");
            var days = Convert.ToInt32((DateTime.Now.AddYears(10) - DateTime.Now).TotalDays); 

            var predictions = system.CalculateConditionsForAPeriod(days);
            predictions.ToList().ForEach(prediction => Console.WriteLine($"{prediction.Key} : {prediction.Value}"));
            Console.WriteLine($"Pico máximo de lluvia: {system.MaxRainyPeriod.ToShortDateString()}");

            Console.ReadKey();
           
        }
        
        private static Processor InitializeSystem()
        {
            var planet1 = new Planet(500, "Ferrengi", 1);
            var planet2 = new Planet(2000, "Betasoide", 3);
            var planet3 = new Planet(1000, "Vulcano", -5);

            var system = new Processor();
            system.Planets.AddRange(new[] { planet1, planet2, planet3 });

            return system;
        }
    }
}
