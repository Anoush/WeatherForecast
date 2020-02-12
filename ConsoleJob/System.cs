using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriPlanetarySystem
{
    public class System
    {
        private readonly List<Planet> planets = new List<Planet>();

        public List<Planet> Planets => planets;
        public void SimulateConditions(DateTime date)
        {
            Planets.ForEach(planet => planet.UpdatePosition(date));
        }

        public bool IsSunWithinPlanetsTriangle()
        {
            var sideAB = MathUtility.CalculateDistanceBetweenCoordinates(Planets[0].Position.x, Planets[0].Position.y, Planets[1].Position.x, Planets[1].Position.y);
            var sideBC = MathUtility.CalculateDistanceBetweenCoordinates(Planets[1].Position.x, Planets[1].Position.y, Planets[2].Position.x, Planets[2].Position.y);
            var sideCA = MathUtility.CalculateDistanceBetweenCoordinates(Planets[2].Position.x, Planets[2].Position.y, Planets[0].Position.x, Planets[0].Position.y);

            var planetTriangleTotalArea = MathUtility.CalculateTriangleArea(sideAB, sideBC, sideCA);
            var internalTrianglesArea = CalculateInternalTriangles(sideAB, sideBC, sideCA);

            return planetTriangleTotalArea == internalTrianglesArea;
        }

        public double CalculateInternalTriangles(double sideAB, double sideBC, double sideCA)
        {
            var centerALentgh = MathUtility.CalculateDistanceBetweenCoordinates(0, 0, Planets[0].Position.x, Planets[0].Position.y);
            var centerBLentgh = MathUtility.CalculateDistanceBetweenCoordinates(0, 0, Planets[1].Position.x, Planets[1].Position.y);
            var centerCLentgh = MathUtility.CalculateDistanceBetweenCoordinates(0, 0, Planets[2].Position.x, Planets[2].Position.y);

            var centerABArea = MathUtility.CalculateTriangleArea(sideAB, centerALentgh, centerBLentgh);
            var centerBCArea = MathUtility.CalculateTriangleArea(sideBC, centerBLentgh, centerCLentgh);
            var centerCAArea = MathUtility.CalculateTriangleArea(sideCA, centerALentgh, centerCLentgh);

            return centerABArea + centerBCArea + centerCAArea;
        }

        public bool CalculatePlanetAlignment(double? sun = null)
        {
            double slope = 0;
            int inline = 0;
            Planets.ForEach(planet =>
                {
                     var linearSlope = MathUtility.CalculateLinearEquation(Planets[0].Position.x, Planets[0].Position.y,
                        Planets[1].Position.x, Planets[1].Position.y);
                     if (slope > 0 && slope == linearSlope) inline ++;
                });

            return inline == Planets.Count;
        }
    }
}
