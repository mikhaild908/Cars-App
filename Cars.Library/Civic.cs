namespace Cars.Library
{
    public class Civic : Car
    {
        public Civic()
        {
            Make = "Honda";
        }

        public Civic(string name, int year) : this()
        {            
            Name = name;
            Year = year;
            Speed = 80;
        }

        public override int Move(int hours)
        {
            return base.Move(hours);
        }
    }
}