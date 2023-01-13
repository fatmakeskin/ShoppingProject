using Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service.Interface
{
    public interface IBasketService
    {
        BasketDto Get(int Id);
        List<BasketDto> GetAll();  
        void Add(BasketDto model);
        void Update(BasketDto model);
        void Delete(BasketDto model);
    }
}
