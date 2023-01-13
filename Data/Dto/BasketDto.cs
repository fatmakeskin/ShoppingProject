using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dto
{
    public class BasketDto
    {
        public int Id { get; set; }

        public ProductDto Product { get; set; }
        public UserDto User { get; set; }
    }
}
