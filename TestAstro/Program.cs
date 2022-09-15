using System;
using AstroMath;
// Low, Kok Wei (M214391)
namespace TestAstro
{
    class Program
    {
        static void Main(string[] args)
        {
            AstroFormula formulaObj = new AstroFormula();
            Console.Write("Star Velociy input:\t");
            Console.WriteLine("Observed wave length 500.1nm and rest wave length 500.0nm");
            Console.WriteLine("Star Velocity output:\t" + formulaObj.MeasureStarVelocity(500.1, 500.0) + " metres per second");
            Console.WriteLine();

            Console.Write("Star Distance input:\t");
            Console.WriteLine("Parallax Angle 0.547");
            Console.WriteLine("Star Distance output:\t" + formulaObj.MeasureStarDistance(0.547) + " parsecs");
            Console.WriteLine();

            Console.Write("Temperature input:\t");
            Console.WriteLine("Celsius 27");
            Console.WriteLine("Temperature output:\t" + formulaObj.MeasureTemperature(27.0) + " Kelvin");
            Console.WriteLine();

            Console.Write("Event Horizon input:\t");
            Console.WriteLine("Blackhole mass 8.2 x 10 power of 36 kg");
            Console.WriteLine("Event Horizon output:\t" + formulaObj.MeasureEventHorizon(8.2, 36) + " meters");
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
