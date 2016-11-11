﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace LinqToDB.Linq
{
    /// <summary>
    /// Linq查询的类
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public interface IExpressionQuery<
#if !SL4
		out
#endif
		T> : IOrderedQueryable<T>, IQueryProvider
	{
		new Expression Expression { get; set; }
		string         SqlText    { get; }
	}
}