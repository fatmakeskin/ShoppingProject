using Business.Service.Interface;
using Data.Dto;
using Data.Entities;
using DataAccess.UoW;
using ProjectUtils.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service.Services
{
    public class OrderService : IOrderService
    {
        private IUnitofWork uow;
        public OrderService(IUnitofWork _uow)
        {
            uow = _uow;
        }

        public void Add(OrderDto model)
        {
            Order ordermodel = new Order()
            {
                OrderDate = model.OrderDate,
                OrderNumber = UniqeIdGenerator.GetUniqueKey(),
                Status = Data.Enums.OrderStatus.OngoingOrders
            };
            uow.GetRepository<Order>().Add(ordermodel);
            uow.SaveChange();
        }

        public void Delete(OrderDto model)
        {
            var data = uow.GetRepository<Order>().Get(x => x.OrderNumber.Equals(model.OrderNumber));
            Order ordermodel = new Order()
            {
                OrderDate = data.OrderDate,
                OrderNumber = data.OrderNumber,
                Status = data.Status
            };
            uow.GetRepository<Order>().Delete(ordermodel);
            uow.SaveChange();
        }

        public OrderDto Get(int Id)
        {
            var ordermodel = uow.GetRepository<Order>().GetById(Id);
            OrderDto dto = new OrderDto()
            {
                OrderDate = ordermodel.OrderDate,
                OrderNumber = ordermodel.OrderNumber,
                Status = ordermodel.Status
            };
            return dto;
        }

        public List<OrderDto> GetAll()
        {
            List<Order> ordermodel = uow.GetRepository<Order>().GetAll();
            List<OrderDto> result = ordermodel.Select(x => new OrderDto()
            {
                OrderDate = x.OrderDate,
                OrderNumber = x.OrderNumber,
                Status = x.Status
            }).ToList();
            return result;
        }

        public void Update(OrderDto model)
        {
            var data = uow.GetRepository<Order>().Get(x => x.OrderNumber.Equals(model.OrderNumber));
            Order ordermodel = new Order()
            {
                OrderDate = model.OrderDate,
                OrderNumber = data.OrderNumber,
                Status = model.Status
            };
            uow.GetRepository<Order>().Update(ordermodel);
            uow.SaveChange();
        }
    }
}
