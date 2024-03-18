namespace Damon.Interfaces
{
    public interface IObjectInfo
    {
        // Unique identifier for the object.
        int ID { get; }

        // Name of the object.
        string Name { get; }

        // Description of the object.
        string Description { get; }

        // A method to retrieve all relevant info about the object as a string.
        public string GetInfo();
    }
}