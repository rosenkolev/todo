using System.Collections.Generic;

using ToDoApp.Infastructure.Domain;

namespace ToDoApp.Infastructure.Repositories
{
    public interface IItemsRepository
    {
        IReadOnlyList<Item> GetAllItems();

        Item Add(Item item);

        void Update(Item item);
    }
}