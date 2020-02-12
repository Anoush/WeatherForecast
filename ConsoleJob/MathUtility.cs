using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriPlanetarySystem
{
    public static class MathUtility
    {
        public static (int x, int y) ConvertPolarToCartesianCoordinates(int polarY, int degrees)
        {
            var x = polarY * Math.Cos(degrees);
            var y = polarY * Math.Sin(degrees);

            return ((int)x, (int)y);
        }

        public static int RotateDegrees(int initialValue, int displacement)
        {
            var degreeDelta = displacement + initialValue;
            if (displacement >=0)
                return degreeDelta > 360 ? degreeDelta - 360 : degreeDelta;
            else
                return degreeDelta < 0 ? degreeDelta + 360 : degreeDelta;
        }

        public static int CalculateTriangleArea(int @base, int height)
        {
            return @base * height / 2;
        }

        /// <summary>
        /// Calculate triangle area given 3 sides, based on Heron's formula
        /// </summary>
        ///remarks A = \sqrt{s(s-a)(s-b)(s-c)},
        /// <returns></returns>
        public static double CalculateTriangleArea(double sideA, double sideB, double sideC)
        {
            var s = CalculateTrianglePerimeter(sideA, sideB, sideC) / 2;
            return Math.Sqrt(s * (s - sideA) * (s - sideB) * (s - sideC));
        }

        public static double CalculateTrianglePerimeter(double sideA, double sideB, double sideC)
        {
            return (sideA + sideB + sideC);
        }

        public static double CalculateDistanceBetweenCoordinates(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        /// <summary>
        /// Calculate the line between two points
        /// </summary>
        /// remarks m = (y2 - y1) / (x2 -x1) 
        /// <param name="x1">value of the first point over X axis</param>
        /// <param name="y1">value of the first point over Y axis</param>
        /// <param name="x2">value of the second point over X axis</param>
        /// <param name="y2">value of the second point over Y axis</param>
        /// <returns>returns the slope of the line</returns>
        public static double CalculateLinearEquation(int x1, int y1, int x2, int y2)
        {
            return Math.Abs((y2 - y1) / (x2 - x1));
        }
    }
}
