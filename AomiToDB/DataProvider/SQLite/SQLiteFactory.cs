using System;
using System.Collections.Specialized;


namespace AomiToDB.DataProvider.SQLite
{
	class SQLiteFactory: IDataProviderFactory
	{
		IDataProvider IDataProviderFactory.GetDataProvider(NameValueCollection attributes)
		{
			return new SQLiteDataProvider();
		}
	}
}
