﻿using System;

namespace AomiToDB
{
	using Linq;

	public interface ITable<
#if !SL4
		out
#endif
		T> : IExpressionQuery<T>
	{

	}
}
