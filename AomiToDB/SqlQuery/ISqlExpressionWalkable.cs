﻿using System;

namespace AomiToDB.SqlQuery
{
	public interface ISqlExpressionWalkable
	{
		ISqlExpression Walk(bool skipColumns, Func<ISqlExpression,ISqlExpression> func);
	}
}
