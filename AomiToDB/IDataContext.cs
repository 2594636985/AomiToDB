﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace AomiToDB
{
	using Linq;
	using Mapping;
	using SqlProvider;

    /// <summary>
    /// 数据库上下文
    /// </summary>
	public interface IDataContext : IDisposable
	{
		string              ContextID         { get; }
		Func<ISqlBuilder>   CreateSqlProvider { get; }
		Func<ISqlOptimizer> GetSqlOptimizer   { get; }
		SqlProviderFlags    SqlProviderFlags  { get; }
		Type                DataReaderType    { get; }
		MappingSchema       MappingSchema     { get; }
		bool                InlineParameters  { get; set; }
		List<string>        QueryHints        { get; }
		List<string>        NextQueryHints    { get; }

		Expression          GetReaderExpression(MappingSchema mappingSchema, IDataReader reader, int idx, Expression readerExpression, Type toType);
		bool?               IsDBNullAllowed    (IDataReader reader, int idx);

		object              SetQuery           (IQueryContext queryContext);
        /// <summary>
        /// 执行非查询语句
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
		int                 ExecuteNonQuery    (object query);
		object              ExecuteScalar      (object query);
		IDataReader         ExecuteReader      (object query);
		void                ReleaseQuery       (object query);

		string              GetSqlText         (object query);
		IDataContext        Clone              (bool forNestedQuery);

		event EventHandler  OnClosing;
	}
}
