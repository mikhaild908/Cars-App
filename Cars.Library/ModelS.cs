namespace Cars.Library
{
    public class ModelS : Car
    {
        public ModelS()
        {
            Make = "Tesla";
        }

        public ModelS(string name, int year) : this()
        {
            Name = name;
            Year = year;
            Speed = 100;
        }

        public override int Move(int hours)
        {
            return base.Move(hours);
        }
    }
}