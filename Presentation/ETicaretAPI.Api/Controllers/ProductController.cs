using ETicaretAPI.Application.Repository.IProduct;
using ETicaretAPI.Application.RquestParameters;
using ETicaretAPI.Application.ViewModels;
using ETicaretAPI.Domain.Etities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;

        public ProductController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Pagination pagiation)
        {
            var totalCount = _productReadRepository.GetAll().Count();
            var products = _productReadRepository.GetAll(false).Skip(pagiation.Page * pagiation.Size).Take(pagiation.Size).Select(p => new
            {
                p.ID,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            }).ToList();

            return Ok(new { totalCount , products});
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(_productReadRepository.GetByIdAsync(id,false));
        }


        [HttpPost]
        public async Task<IActionResult> Post(Vm_Create_Product model)
        {
            if (ModelState.IsValid)
            {

            }

           await _productWriteRepository.AddAsync(new()
           {
               Name=model.Name,
               Price=model.Price,
               Stock=model.Stock
           });
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(Vm_Update_Product model)
        {
           Product product= await _productReadRepository.GetByIdAsync(model.Id);
            product.Name=model.Name;
            product.Price=model.Price;
            product.Stock=model.Stock;
            await _productWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }



    }
}
