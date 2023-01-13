using Business.Service.Interface;
using Data.Dto;
using Data.Entities;
using DataAccess.UoW;
using Newtonsoft.Json;
using RabbitmqService.Base;
using RabbitmqService.Consumer;
using RabbitmqService.Producer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service.Services
{
    public class BasketService : IBasketService
    {
        private IUnitofWork uow;
        public BasketService(IUnitofWork _uow)
        {
            uow = _uow;
        }

        public void Add(BasketDto model)
        {
            RequestProducer producer = new RequestProducer();
            producer.PublishData(JsonConvert.SerializeObject(model), model.Product.ProductName + model.Product.ProductCount.ToString());
        }

        public void Delete(BasketDto model)
        {
            Basket basketmodel = new Basket()
            {
                Id = model.Id,
            };
            uow.GetRepository<Basket>().Delete(basketmodel);
            uow.SaveChange();
        }

        public BasketDto Get(int Id)
        {
            var basketmodel = uow.GetRepository<Basket>().GetById(Id);
            BasketDto dto = new BasketDto()
            {
                Id = basketmodel.Id
            };
            return dto;
        }

        public List<BasketDto> GetAll()
        {
            List<Basket> basketmodel = uow.GetRepository<Basket>().GetAll();
            List<BasketDto> result = basketmodel.Select(x => new BasketDto()
            {
                Id = x.Id
            }).ToList();
            return result;
        }

        public void Update(BasketDto model)
        {
            Basket basketmodel = new Basket()
            {
                Id = model.Id,
            };
            uow.GetRepository<Basket>().Update(basketmodel);
            uow.SaveChange();
        }
    }
}
