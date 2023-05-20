using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Infrastuctre.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderServices _orderServices;
        private readonly IMapper _mapper;

        public OrdersController(IOrderServices orderServices,IMapper mapper)
        {
            _orderServices=orderServices;
            _mapper=mapper;
        }
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User?.ReteriveEmailPrenciples();
            var address = _mapper.Map<AddressDto,Address>(orderDto.ShipToAddress);
            var order = await _orderServices.CreateOrderAsync(email,orderDto.DeliveryMethodID,orderDto.BasketID,address);
            if (order == null) return BadRequest(new ApiResponce(400, "problem to creat order"));
            return Ok(order);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var email = HttpContext.User?.ReteriveEmailPrenciples();
            var orders=_orderServices.GetOrdersForUserAsync(email);
            return Ok(_mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIDForUser(int id)
        {
            var email = HttpContext.User?.ReteriveEmailPrenciples();
            var order = _orderServices.GetOrdrByIDAsync(id, email);
            if (order == null) return NotFound(new ApiResponce(404));
            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }
        [HttpGet("deliverymethod")]
        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethod()
        {
            return (IReadOnlyList<DeliveryMethod>)Ok(await _orderServices.GetDeliveryMethodsAsync());  
        }
    }
}
