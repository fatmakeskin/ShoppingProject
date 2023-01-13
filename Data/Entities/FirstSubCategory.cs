using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class FirstSubCategory
    {
        public int Id { get; set; }
        public string FirstSubCategoryName { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List<SecondSubCategory> SecondSubCategory { get; set; }
    }
}
