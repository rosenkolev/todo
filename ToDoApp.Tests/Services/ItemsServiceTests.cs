using Microsoft.VisualStudio.TestTools.UnitTesting;

using NSubstitute;

using ToDoApp.Business.Services;
using ToDoApp.Infastructure.Domain;
using ToDoApp.Infastructure.Repositories;
using ToDoApp.Infastructure.Services;

namespace ToDoApp.Test.Repositories
{
    [TestClass]
    public sealed class ItemsServiceTests
    {
        private IItemsRepository _itemsRepository;
        private ItemsService _itemsService;

        [TestInitialize]
        public void Initialize()
        {
            _itemsRepository = Substitute.For<IItemsRepository>();
            _itemsService = new ItemsService(_itemsRepository);
        }

        [TestMethod]
        public void GetActiveItemsReturnOnlyActive()
        {
            _itemsRepository.GetAllItems().Returns(
                new[]
                {
                    new Item("0", "A", false),
                    new Item("1", "B", true),
                    new Item("2", "C", false),
                });

            var result = _itemsService.GetActiveItems();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("B", result[0].Name);
        }
    }
}
