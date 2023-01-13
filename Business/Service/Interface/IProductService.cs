using Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service.Interface
{
    public interface IProductService
    {
        ProductDto Get(int Id);
        List<ProductDto> GetAll();
        void Add(ProductDto model);
        void Update(ProductDto model);
        void Delete(ProductDto model);
        List<ProductDto> SearchByName(string name);
    }
}
