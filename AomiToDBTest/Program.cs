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
            //LinqToDB.Common.Configuration.Linq.AllowMultipleQuery = true;
            using (AdoContext dbContext = new AdoContext())
            {

            //    //var query = from s in dbContext.Suppliers where s.City == "London" select (from p in dbContext.Products where p.SupplierID == s.SupplierID select p);

            //    //List<Products> catalogsList = query.Single().ToList();

            //    //dbContext.QueryHints.Add("---");
            //    //dbContext.NextQueryHints.Add("----");



            //    //var query = dbContext.Suppliers.Where(s => s.Products.Sum(p => p.UnitPrice) > 30);

            //    ////List<Products> catalogsList = query.ToList();
            //    //List<Suppliers> suppliers = query.ToList();

            //    //var query = (from p in dbContext.Products where p.ProductID == 20 select p).Concat(from p in dbContext.Products where p.ProductID == 20 select p);

            //    //List<Products> catalogsList = query.ToList();


            //    //Products product = (from p in dbContext.Products where p.ProductID == 30 select p).Single();

            //    //var query = from p in dbContext.Products group p by p.SupplierID into g where g.Sum( t=> t.UnitPrice) > 20 select g.Key;

            //    //List<int> resultList = query.ToList();

            int n2 = 30;
            var query = from p in dbContext.Products where p.ProductID > n2 select p;
            List<Products> productList = query.ToList();


            //    //var query = from p in dbContext.Products
            //    //            join s in dbContext.Suppliers on p.SupplierID equals s.SupplierID
            //    //            where s.City == "London"
            //    //            select new Products() { ProductID = p.ProductID, ProductName = p.ProductName + ":" + s.CompanyName };

            //    //List<Products> catalogsList = query.ToList();
            }
            //var query = CompiledQuery.Compile((IAdoContext db, int n2) =>
            //   db.Products.Where(p => p.ProductID > n2));
            //using (AdoContext dbContext = new AdoContext())
            //{
            //    List<Products> catalogsList = query(dbContext, 30).ToList();

            //    dbContext.Products.Where(t => t.ProductID > 30).Count();
            //}
            //Expression<Func<object, string>> expr = (a) => (string)a;
            //LambdaExpression lExpr = expr as LambdaExpression;

        }

        public static bool MyWhere(Products p)
        {
            return p.ProductID > 20;
        }
        public static Expression<Func<Products, bool>> SelectAnyExpression()
        {
            return p => p.ProductID > 0 && p.ProductID > 30;
        }
    }
}
