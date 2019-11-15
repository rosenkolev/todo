using System;
using System.Collections.Generic;

using ToDoApp.Infastructure.Domain;
using ToDoApp.Infastructure.Repositories;

namespace ToDoApp.Data.Repositories
{
    public sealed class ItemsRepository : IItemsRepository
    {
        private static readonly List<Item> Items = new List<Item>
        {
            new Item("28bafe6e-70ce-4ad5-97ad-ee25cb2fac6b", "Item 1", false),
            new Item("e094fce1-1823-48d1-a0c8-eb86ff40f3c3", "Item 2", true),
            new Item("3294b1d7-17c8-4106-9b22-9f4ca68dcc43", "Item 3", true)
        };

        /// <inheritdoc />
        public IReadOnlyList<Item> GetAllItems() =>
            Items;

        public Item Add(Item item)
        {
            item = new Item(Guid.NewGuid().ToString(), item.Name, item.IsActive);
            Items.Add(item);
            return item;
        }

        public void Update(Item item)
        {
            Items.RemoveAll(it => it.Id == item.Id);
            Items.Add(item);
        }
    }
}