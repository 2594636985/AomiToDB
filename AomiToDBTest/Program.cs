using LinqToDB.SqlQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Data;

namespace AomiToDBTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (AdoContext dbContext = new AdoContext())
            {
                var query = from p in dbContext.Products where p.ProductID == 30 select p;
                List<Products> catalogsList = query.ToList();
                
                
            }
        }
    }
}
