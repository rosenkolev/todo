using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using ToDoApp.Infastructure.Models;
using ToDoApp.Infastructure.Services;

namespace ToDoApp.Presentation.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        private readonly IItemsService _itemsService;
        
        public ItemsController(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        [HttpGet]
        public IReadOnlyList<ItemModel> Get() =>
            _itemsService.GetActiveItems();

        [HttpPost]
        public IActionResult Post([FromBody] ItemModel item)
        {
            var addedItem = _itemsService.Update(item);
            if (addedItem != null)
            {
                var result = new ObjectResult(addedItem);
                result.StatusCode = 201;
                return result;
            }

            return Ok(item);
        }
    }
}
