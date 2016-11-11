﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

using JetBrains.Annotations;

namespace LinqToDB.Linq
{
    using Extensions;

    abstract class ExpressionQuery<T> : IExpressionQuery<T>
    {
        #region Init

        protected void Init(IDataContextInfo dataContextInfo, Expression expression)
        {
#if SILVERLIGHT || NETFX_CORE
			if (dataContextInfo == null) throw new ArgumentNullException("dataContextInfo");

			DataContextInfo = dataContextInfo;
#else
            DataContextInfo = dataContextInfo ?? new DefaultDataContextInfo();
#endif
            Expression = expression ?? Expression.Constant(this);
        }

        [NotNull]
        public Expression Expression { get; set; }
        [NotNull]
        public IDataContextInfo DataContextInfo { get; set; }

        internal Query<T> AomiInfo;
        internal object[] Parameters;

        #endregion

        #region Public Members

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _sqlTextHolder;

        [UsedImplicitly]
        private string _sqlText { get { return SqlText; } }

        /// <summary>
        /// 获得当前的SQL语句
        /// </summary>
        public string SqlText
        {
            get
            {
                var hasQueryHints = DataContextInfo.DataContext.QueryHints.Count > 0 || DataContextInfo.DataContext.NextQueryHints.Count > 0;

                if (_sqlTextHolder == null || hasQueryHints)
                {
                    var info = GetQuery(Expression, true);
                    var sqlText = info.GetSqlText(DataContextInfo.DataContext, Expression, Parameters, 0);

                    if (hasQueryHints)
                        return sqlText;

                    _sqlTextHolder = sqlText;
                }

                return _sqlTextHolder;
            }
        }

        #endregion

        #region Execute
        /// <summary>
        /// 用于最后执行对应的表达式
        /// </summary>
        /// <param name="dataContextInfo"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        IEnumerable<T> Execute(IDataContextInfo dataContextInfo, Expression expression)
        {
            return GetQuery(expression, true).GetIEnumerable(null, dataContextInfo, expression, Parameters);
        }


         Query<T> GetQuery(Expression expression, bool cache)
        {
            if (cache && AomiInfo != null)
                return AomiInfo;

            var info = Query<T>.GetQuery(DataContextInfo, expression);

            if (cache)
                AomiInfo = info;

            return info;
        }

        #endregion

        #region IQueryable接口的成员

        Type IQueryable.ElementType
        {
            get
            {
                return typeof(T);
            }
        }

        Expression IQueryable.Expression
        {
            get
            {
                return Expression;
            }
        }

        IQueryProvider IQueryable.Provider
        {
            get
            {
                return this;
            }
        }

        #endregion

        #region IQueryProvider接口的成员

        IQueryable<TElement> IQueryProvider.CreateQuery<TElement>(Expression expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            return new ExpressionQueryImpl<TElement>(DataContextInfo, expression);
        }

        IQueryable IQueryProvider.CreateQuery(Expression expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            var elementType = expression.Type.GetItemType() ?? expression.Type;

            try
            {
                return (IQueryable)Activator.CreateInstance(typeof(ExpressionQueryImpl<>).MakeGenericType(elementType), new object[] { DataContextInfo, expression });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        TResult IQueryProvider.Execute<TResult>(Expression expression)
        {
            return (TResult)GetQuery(expression, false).GetElement(null, DataContextInfo, expression, Parameters);
        }

        object IQueryProvider.Execute(Expression expression)
        {
            return GetQuery(expression, false).GetElement(null, DataContextInfo, expression, Parameters);
        }

        #endregion

        #region IEnumerable接口的成员

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            IEnumerable<T> result = Execute(DataContextInfo, Expression);
            IEnumerator<T> iEnumerator = result.GetEnumerator();
            return iEnumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Execute(DataContextInfo, Expression).GetEnumerator();
        }

        #endregion
    }
}