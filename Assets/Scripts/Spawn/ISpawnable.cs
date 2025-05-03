namespace Spawn
{
    public interface ISpawnable
    {
        public string Name { get; set; }

        public void Initialize(); // configs the new object (from GetSpawnable)
    }
}