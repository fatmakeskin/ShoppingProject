using Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service.Interface
{
    public interface ICategoryService
    {
        CategoryDto Get(int Id);
        List<CategoryDto> GetAll();
        void Add(CategoryDto model);
        void Update(CategoryDto model);
        void Delete(CategoryDto model);
        CategoryDto SearchByName(string name);
    }
}
