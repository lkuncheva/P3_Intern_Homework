namespace _04._02
{
    public class Path
    {
        public List<Point3D> Points3DList { get; set; }

        public Path()
        {
            this.Points3DList = new List<Point3D>();
        }

        public void AddPoint(Point3D point)
        {
            this.Points3DList.Add(point);
        }
    }
}