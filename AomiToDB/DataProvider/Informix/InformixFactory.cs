using System;
using System.Collections.Specialized;


namespace AomiToDB.DataProvider.Informix
{
	class InformixFactory : IDataProviderFactory
	{
		IDataProvider IDataProviderFactory.GetDataProvider(NameValueCollection attributes)
		{
			return new InformixDataProvider();
		}
	}
}
