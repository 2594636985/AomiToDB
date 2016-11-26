using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AomiToDB.Mapping;

namespace AomiToDBTest
{
    public class Suppliers
    {
        [PrimaryKey, Identity]
        public int SupplierID { set; get; }
        [Column]
        public string CompanyName { set; get; }
        [Column]
        public string City { set; get; }

        [Association(ThisKey = "SupplierID", OtherKey = "SupplierID")]
        public List<Products> Products { set; get; }
    }
}
