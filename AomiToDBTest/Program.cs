using AomiToDB.SqlQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AomiToDB;
using AomiToDB.Data;
using System.Linq.Expressions;

namespace AomiToDBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (AdoContext dbContext = new AdoContext())
            {
            int n2 = 30;
            var query = from p in dbContext.Products where p.ProductID > n2 select p;
            List<Products> productList = query.ToList();

            }
        }
    }
}
