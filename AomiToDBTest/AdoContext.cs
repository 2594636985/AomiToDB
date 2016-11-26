using AomiToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AomiToDBTest
{
    public class AdoContext : DataContext, IAdoContext
    {
        public ITable<Products> Products
        {
            get
            {
                return this.GetTable<Products>();
            }
        }
        public ITable<Suppliers> Suppliers
        {
            get
            {
                return this.GetTable<Suppliers>();
            }
        }
    }

    public interface IAdoContext : IDataContext
    {
        ITable<Products> Products
        {
            get;
        }
    }
}
