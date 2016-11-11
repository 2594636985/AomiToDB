using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AomiToDBTest
{
    public class AdoContext : DataContext
    {
        public ITable<Products> Products
        {
            get
            {
                return this.GetTable<Products>();
            }
        }
    }
}
