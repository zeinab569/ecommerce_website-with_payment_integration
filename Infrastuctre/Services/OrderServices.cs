using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;
using Infrastuctre.Data;
using Infrastuctre.Data.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastuctre.Services
{
    public class OrderServices : IOrderServices
    {

        private readonly IUnitOfWork _unitOfWork;
        //private readonly IGenericRepo<Order> _orderRepo;
        //private readonly IGenericRepo<DeliveryMethod> _deliveryRepo;
        //private readonly IGenericRepo<Product> _productRepo;
        private readonly IBasketRepo _basketRepo;

        public OrderServices( IBasketRepo basketRepo,IUnitOfWork unitOfWork)
            
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodid, string basketId, Address shippingAddress)
        {
            //get the basket from basketrepo
            var basket = await _basketRepo.GetBasketAsync(basketId);
            //Get items from product 
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productitem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemorderd = new ProductItemOrderd(productitem.Id, productitem.Name, productitem.PictureUrl);
                var orderitem = new OrderItem(itemorderd, productitem.Price, item.Quantity);
                items.Add(orderitem);
            }
            //get deliverymethod 
            var delvarymethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodid);
            //calculate subtotal
            var subtotal = items.Sum(item=>item.Price * item.Quantity);
            //create order
            var order = new Order(items,buyerEmail,shippingAddress,delvarymethod,subtotal);
            _unitOfWork.Repository<Order>().Add(order);
            //TODO save to db
            var result = await _unitOfWork.Complete();

            //Delete basket
            await _basketRepo.DeleteBasketAsync(basketId);
            // return order 
            if (result <= 0) return null;
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().GetListAsync();
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderWithItemsAndOrdringSpecification(buyerEmail);
            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }

        public async Task<Order> GetOrdrByIDAsync(int orderid, string buyerEmail)
        {
            var spec = new OrderWithItemsAndOrdringSpecification(orderid, buyerEmail);  
            return await _unitOfWork.Repository<Order>().GetWithSpec(spec);
        }
    }
}
