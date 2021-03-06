﻿using System;
using System.Linq.Expressions;

namespace AomiToDB.Linq
{
	class ExpressionQueryImpl<T> : ExpressionQuery<T>, IExpressionQuery
	{
		public ExpressionQueryImpl(IDataContextInfo dataContext, Expression expression)
		{
			Init(dataContext, expression);
		}

		public override string ToString()
		{
			return SqlText;
		}
	}
}
