using System;

namespace Library
{
    public class Planet
    {
        private (int x, int y) position = (0, 0);
        private int headingDegrees;
        private DateTime lastCalculatedDate = DateTime.Now;

        public Planet(int distanceToSun, string name, int angularSpeed)
        {
            BuildObject(distanceToSun, name, angularSpeed);
        }

        public string Name { get; protected set; }
        public int AngularSpeed { get; protected set; }
        public (int x, int y) Position
        {
            get
            {
                return position;
            }
            set
            {
                this.position = value;
            }
        }

        public int HeadingDegrees
        {
            get => headingDegrees;
            set => headingDegrees = value;
        }
       
        //inicio del calculo. (cuando comienza a existir)
        protected DateTime LastCalculatedDate { get => lastCalculatedDate; set => lastCalculatedDate = value; }
        public int DistanceToSun { get; protected set; }

        public void UpdatePosition(DateTime date)
        {
            var daysToCalculate = Math.Ceiling((date - LastCalculatedDate).TotalDays);
            var degreeDelta = ((int)daysToCalculate * AngularSpeed) % 360;
            HeadingDegrees = MathUtility.RotateDegrees(HeadingDegrees, degreeDelta);
            Position = MathUtility.ConvertPolarToCartesianCoordinates(DistanceToSun, headingDegrees);

            LastCalculatedDate = date;
        }

        public void BuildObject(int distanceToSun, string name, int angularSpeed)
        {
            DistanceToSun = distanceToSun;
            Name = name;
            AngularSpeed = angularSpeed;

            position = (0, distanceToSun);
        }
    }
}
