using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class SecondSubCategory
    {
        public int Id { get; set; }
        public string SecondSubCategoryName { get; set; }

        public int FirstSubCategoryId { get; set; }
        public FirstSubCategory FirstSubCategory { get; set; }
    }
}
