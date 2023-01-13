using Business.Service.Interface;
using Data.Dto;
using Data.Entities;
using Data.Enums;
using DataAccess.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service.Services
{
    public class WalletService : IWalletService
    {
        private IUnitofWork uow;
        public WalletService(IUnitofWork _uow)
        {
            uow = _uow;
        }

        public void AddMoney(WalletDto model)
        {
            Wallet walletmodel = new Wallet()
            {
                Money = model.Money,
                AccountSummary = model.AccountSummary,
                UserId = model.UserId,
                
            };
            uow.GetRepository<Wallet>().Add(walletmodel);
            uow.SaveChange();
        }

        public void UpdateMoney(WalletDto model)
        {
            var data = uow.GetRepository<Wallet>().GetById(model.Id);
            data.Money = model.Money;

            uow.GetRepository<Wallet>().Update(data);
        }

        public List<WalletDto> GetSummary(int id)
        {
            var data = uow.GetRepository<Wallet>().FindAll(x => x.UserId.Equals(id));

            List<WalletDto> walletList = data.Select(x=> new WalletDto()
            {
                Id = x.Id,
                UserId = x.UserId,
                AccountSummary = x.AccountSummary,
                Money = x.Money,
            }).ToList();
            return walletList;
        }
    }
}
