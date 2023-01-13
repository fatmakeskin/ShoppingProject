using Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service.Interface
{
    public interface IOrderService
    {
        OrderDto Get(int Id);
        List<OrderDto> GetAll();
        void Add(OrderDto model);
        void Update(OrderDto model);
        void Delete(OrderDto model);
    }
}
