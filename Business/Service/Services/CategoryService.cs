using Business.Service.Interface;
using Data.Dto;
using Data.Entities;
using DataAccess.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private IUnitofWork uow;
        public CategoryService(IUnitofWork _uow)
        {
            uow = _uow;
        }

        public void Add(CategoryDto model)
        {
            Category category = new Category() 
            { 
                CategoryName = model.CategoryName                
            };
            uow.GetRepository<Category>().Add(category);
            uow.SaveChange();

        }

        public void Delete(CategoryDto model)
        {
            Category category = new Category()
            {
                CategoryName = model.CategoryName
            };
            uow.GetRepository<Category>().Delete(category);
            uow.SaveChange();
        }

        public CategoryDto Get(int Id)
        {
            var category = uow.GetRepository<Category>().GetById(Id);
            CategoryDto dto = new CategoryDto()
            {
                CategoryName = category.CategoryName
            };
            return dto;

        }

        public List<CategoryDto> GetAll()
        {
            List<Category> categorymodel = uow.GetRepository<Category>().GetAll();
            List<CategoryDto> result = categorymodel.Select(x => new CategoryDto()
            {
                CategoryName=x.CategoryName   

            }).ToList();
            return result;
        }
        public void Update(CategoryDto model)
        {
            Category category = new Category()
            {
                CategoryName = model.CategoryName
            };
            uow.GetRepository<Category>().Update(category);
            uow.SaveChange();
        }
        public CategoryDto SearchByName(string name)
        {

            var data = uow.GetRepository<Category>().Get(x => x.CategoryName.Equals(name));
            CategoryDto dto = new CategoryDto()
            {
                CategoryName = data.CategoryName
            };
            return dto;
        }
    }
}
