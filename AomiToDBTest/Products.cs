using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AomiToDB.Mapping;
using AomiToDB;
using System.Linq.Expressions;

namespace AomiToDBTest
{
    public class Products
    {
        [PrimaryKey, Identity]
        public int ProductID { set; get; }
        [Column, ExpressionMethod("ValidateProductName")]
        public string ProductName { set; get; }
        [Column]
        public decimal UnitPrice { set; get; }
        [Column]
        public int SupplierID { set; get; }

        [Association(ThisKey = "SupplierID", OtherKey = "SupplierID")]
        public Suppliers Suppliers { set; get; }

        public static Expression<Func<string, string>> ValidateProductName()
        {
            return n => "aomi";
        }
    }
}
