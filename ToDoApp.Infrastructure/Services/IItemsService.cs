using System.Collections.Generic;

using ToDoApp.Infastructure.Models;

namespace ToDoApp.Infastructure.Services
{
    public interface IItemsService
    {
        IReadOnlyList<ItemModel> GetActiveItems();

        ItemModel Update(ItemModel item);
    }
}
