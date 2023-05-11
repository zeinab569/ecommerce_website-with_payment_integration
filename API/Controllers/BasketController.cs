using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Infrastuctre.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepo _basketrepo;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepo basketrepo,IMapper mapper)
        {
            _basketrepo = basketrepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketByID(string id) 
        {
            var data = await _basketrepo.GetBasketAsync(id);
            return Ok(data ?? new CustomerBasket(id)); 
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket)
        {
            var customerBasket = _mapper.Map<CustomerBasket>(basket);
            var updated= await _basketrepo.UpdateBasketAsync(customerBasket);
            return Ok(updated);
        }
        [HttpDelete]
        public async Task DeletedBasket(string id)
        {
            await _basketrepo.DeleteBasketAsync(id);
        }
    }
}
