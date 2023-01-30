using Catalog.Dto.Categories;
using Catalog.Service.Services.Categories.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : GenericController
    {
        public CategoriesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Insert(CategoryInsertDto categoryInsertDto)
        {
            return Ok(await _mediator.Send(new CategoryInsertCommand() { CategoryInsertDto = categoryInsertDto }));
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            return Ok(await _mediator.Send(new CategoryUpdateCommand() { CategoryUpdateDto = categoryUpdateDto }));
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(CategoryDeleteDto categoryDeleteDto)
        {
            return Ok(await _mediator.Send(new CategoryDeleteCommand() { CategoryDeleteDto = categoryDeleteDto }));
        }



    }
}
