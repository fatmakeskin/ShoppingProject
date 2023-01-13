using Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Service.Interface
{
    public interface IWalletService
    {
        void AddMoney(WalletDto model);
        List<WalletDto> GetSummary(int id);
        void UpdateMoney(WalletDto model);

    }
}
