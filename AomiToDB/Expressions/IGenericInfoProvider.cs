using System;

namespace AomiToDB.Expressions
{
	using Mapping;

	public interface IGenericInfoProvider
	{
		void SetInfo(MappingSchema mappingSchema);
	}
}
