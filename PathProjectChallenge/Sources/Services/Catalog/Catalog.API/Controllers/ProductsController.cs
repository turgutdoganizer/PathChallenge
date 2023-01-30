using Catalog.Dto.Products;
using Catalog.Service.Services.Products.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : GenericController
    {
        public ProductsController(IMediator mediator) : base(mediator)
        {
        }


        [HttpPost]
        public async Task<IActionResult> Insert(ProductInsertDto productInsertDto)
        {
            return Ok(await _mediator.Send(new ProductInsertCommand() { ProductInsertDto = productInsertDto }));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            return Ok(await _mediator.Send(new ProductUpdateCommand() { ProductUpdateDto = productUpdateDto }));
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(ProductDeleteDto productDeleteDto)
        {
            return Ok(await _mediator.Send(new ProductDeleteCommand() { ProductDeleteDto = productDeleteDto }));
        }

    }
}
