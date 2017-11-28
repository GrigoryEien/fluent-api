namespace ObjectPrinting.Tests
{
    public class NestedObject
    {
        public NestedObject Child{ get; set; }

        public NestedObject(NestedObject child, int id, double weight, float height, string name)
        {
            Child = child;
            Id = id;
            Weight = weight;
            Height = height;
            Name = name;
        }

        public int Id { get; set; }
        public double Weight { get; set; }
        public float Height { get; set; }
        public string Name { get; set; }
    }
}