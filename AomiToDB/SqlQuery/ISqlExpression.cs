﻿using System;

namespace AomiToDB.SqlQuery
{
	public interface ISqlExpression : IQueryElement, IEquatable<ISqlExpression>, ISqlExpressionWalkable, ICloneableElement
	{
		bool Equals   (ISqlExpression other, Func<ISqlExpression,ISqlExpression,bool> comparer);

		bool CanBeNull  { get; }
		int  Precedence { get; }
		Type SystemType { get; }
	}
}
