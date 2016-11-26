using System;
using System.Linq.Expressions;

namespace AomiToDB.Linq
{
	using Mapping;

	public interface IExpressionInfo
	{
		LambdaExpression GetExpression(MappingSchema mappingSchema);
	}
}
