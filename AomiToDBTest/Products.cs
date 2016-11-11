using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace AomiToDBTest
{
    public class Products
    {
        [PrimaryKey, Identity]
        public int ProductID { set; get; }
        [Column]
        public string ProductName { set; get; }
        [Column]
        public decimal UnitPrice { set; get; }
    }
}
