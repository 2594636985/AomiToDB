using System;

namespace AomiToDB.SqlQuery
{
	public interface ISqlPredicate : IQueryElement, ISqlExpressionWalkable, ICloneableElement
	{
		bool CanBeNull  { get; }
		int  Precedence { get; }
	}
}
