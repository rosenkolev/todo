using System.Collections.Generic;
using System.Linq;

using ToDoApp.Infastructure.Domain;
using ToDoApp.Infastructure.Models;
using ToDoApp.Infastructure.Repositories;
using ToDoApp.Infastructure.Services;

namespace ToDoApp.Business.Services
{
    public sealed class ItemsService : IItemsService
    {
        private readonly IItemsRepository _itemsRepository;

        public ItemsService(IItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        /// <inheritdoc />
        public IReadOnlyList<ItemModel> GetActiveItems() =>
            _itemsRepository
                .GetAllItems()
                .Where(it => it.IsActive)
                .Select(ToModel)
                .ToArray();

        /// <inheritdoc />
        public ItemModel Update(ItemModel item)
        {
            if (item.Id == null)
            {
                return ToModel(
                    _itemsRepository.Add(
                        new Item(null, item.Name, true)));
            }

            _itemsRepository.Update(
                new Item(item.Id, item.Name, false));

            return null;
        }

        private static ItemModel ToModel(Item item) =>
            new ItemModel
            {
                Id = item.Id,
                Name = item.Name
            };
    }
}