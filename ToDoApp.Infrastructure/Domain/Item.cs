namespace ToDoApp.Infastructure.Domain
{
    public sealed class Item
    {
        public Item(string id, string name, bool isActive)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
        }

        public string Id { get; }

        public string Name { get; }

        public bool IsActive { get; }
    }
}
