namespace Backend.Domains.DiceDomain
{
    public class Dice
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public Dice()
        {
            Min = 1;
            Max = 6;
        }

    }
}
