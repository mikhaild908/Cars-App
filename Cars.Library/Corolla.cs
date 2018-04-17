namespace Cars.Library
{
    public class Corolla : Car
    {
        public Corolla()
        {
            Make = "Toyota";
        }

        public Corolla(string name, int year) : this()
        {
            Name = name;
            Year = year;
            Speed = 60;
        }

        public override int Move(int hours)
        {
            return base.Move(hours);
        }

        public override void Paint()
        {
            base.Paint();

            Color = "Red";
        }
    }
}