using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Basket
    {

        public int Id { get; set; }

        public List<Product> Product { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
