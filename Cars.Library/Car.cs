namespace Cars.Library
{
    public abstract class Car
    {
        public string Name { get; set; }
        public string Make { get; set; }
        public int Year { get; set; }
        public int DistanceTravelled { get; private set; }
        public int Speed { get; protected set; }

        public string Color { get; set; }

        public virtual int Move(int hours)
        {
            DistanceTravelled = Speed * hours;

            return DistanceTravelled;
        }        

        public virtual void Paint()
        {
            Color = "Black";
        }
    }
}