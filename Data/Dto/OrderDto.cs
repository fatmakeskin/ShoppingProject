using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Data.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        [JsonIgnore]
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }

        public UserDto User { get; set; }
        public ProductDto Product { get; set; }
    }
}
