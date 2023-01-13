using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto
{
    public class WalletDto
    {
        public int Id { get; set; }
        public int Money { get; set; }
        public string AccountSummary { get; set; }
        public int UserId { get; set; }
    }
}
