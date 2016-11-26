using System;
using System.Collections.Specialized;


namespace AomiToDB.DataProvider.PostgreSQL
{
    class PostgreSQLFactory : IDataProviderFactory
    {
        IDataProvider IDataProviderFactory.GetDataProvider(NameValueCollection attributes)
        {
            return new PostgreSQLDataProvider();
        }
    }
}
