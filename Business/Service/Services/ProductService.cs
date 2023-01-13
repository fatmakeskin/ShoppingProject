using Business.Service.Interface;
using Data.Dto;
using Data.Entities;
using DataAccess.UoW;
using ProjectUtils.Helpers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service.Services
{
    public class ProductService : IProductService
    {
        private IUnitofWork uow;
        public ProductService(IUnitofWork _uow)
        {
            uow = _uow;
        }

        public void Add(ProductDto model)
        {
            Product productmodel = new Product()
            {
                ProductName = model.ProductName,
                ProductCount = model.ProductCount,
                Price = model.Price,
                Image = ImageConverter.ImageConvert(model.Image),
            };
            uow.GetRepository<Product>().Add(productmodel);
            uow.SaveChange();
        }

        public void Delete(ProductDto model)
        {
            var data = uow.GetRepository<Product>().GetById(model.Id);

            data.ProductName = model.ProductName;
            data.ProductCount = model.ProductCount;
            data.Price = model.Price;
            data.Image = ImageConverter.ImageConvert(model.Image);

            uow.GetRepository<Product>().Delete(data);
            uow.SaveChange();
        }
        public void Update(ProductDto model)
        {
            var data = uow.GetRepository<Product>().GetById(model.Id);

            data.ProductName = model.ProductName;
            data.ProductCount = model.ProductCount;
            data.Price = model.Price;
            data.Image = ImageConverter.ImageConvert(model.Image);

            uow.GetRepository<Product>().Update(data);
            uow.SaveChange();
        }

        public ProductDto Get(int Id)
        {
            Product model = uow.GetRepository<Product>().GetById(Id);
            ProductDto result = new ProductDto()
            {
                ProductName = model.ProductName,
                ProductCount = model.ProductCount,
                Price = model.Price,
                Image = ImageConverter.ImageConvert(model.Image)
            };
            return result;
        }

        public List<ProductDto> GetAll()
        {
            List<Product> model = uow.GetRepository<Product>().GetAll();
            List<ProductDto> result = model.Select(x => new ProductDto()
            {
                ProductName = x.ProductName,
                ProductCount = x.ProductCount,
                Price = x.Price,
                Image = x.Image
            }).ToList();
            return result;
        }

        public List<ProductDto> SearchByName(string name)
        {

            var data = uow.GetRepository<Product>().FindAll(x => x.ProductName.Equals(name));
            List<ProductDto> dto = data.Select(x => new ProductDto()
            {
                ProductName = x.ProductName,
                ProductCount = x.ProductCount,
                Price = x.Price,
                Image = x.Image

            }).ToList();
            return dto;
        }
    }
}
