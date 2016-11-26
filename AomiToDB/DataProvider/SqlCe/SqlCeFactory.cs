using System;
using System.Collections.Specialized;


namespace AomiToDB.DataProvider.SqlCe
{
	class SqlCeFactory : IDataProviderFactory
	{
		IDataProvider IDataProviderFactory.GetDataProvider(NameValueCollection attributes)
		{
			return new SqlCeDataProvider();
		}
	}
}
