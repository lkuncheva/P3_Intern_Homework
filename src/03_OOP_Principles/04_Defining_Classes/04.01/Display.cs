namespace _04._01
{
    public class Display
    {
        private double? size;
        private int? numberOfColours;

        public double? Size
        {
            get { return size; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Display size cannot be negative.");
                }

                size = value;
            }
        }

        public int? NumberOfColours
        {
            get { return numberOfColours; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Number of colors cannot be negative.");
                }

                numberOfColours = value;
            }
        }

        public Display()
        {
            this.size = null;
            this.numberOfColours = null;
        }

        public Display(double? size) : this(size, null) { }

        public Display(double? size, int? numberOfColors)
        {
            this.Size = size;
            this.NumberOfColours = numberOfColors;
        }

        public override string ToString()
        {
            return $"Display: Size = {Size ?? 0} inches, Colors = {NumberOfColours ?? 0}";
        }
    }
}