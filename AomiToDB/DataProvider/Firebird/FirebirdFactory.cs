using System;
using System.Collections.Specialized;


namespace AomiToDB.DataProvider.Firebird
{
	class FirebirdFactory: IDataProviderFactory
	{
		IDataProvider IDataProviderFactory.GetDataProvider(NameValueCollection attributes)
		{
			return new FirebirdDataProvider();
		}
	}
}
