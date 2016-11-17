﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace LinqToDB.Mapping
{
    using Common;
    using Expressions;
    using Extensions;
    using Metadata;
    using SqlQuery;

    class MappingSchemaInfo
    {
        #region 私有字段
        private volatile ConcurrentDictionary<Type, bool> _canBeNull;
        private volatile ConcurrentDictionary<Type, object> _defaultValues;
        #endregion
        #region 公有属性
        public string Configuration;
        public IMetadataReader MetadataReader;
        #endregion
        public MappingSchemaInfo(string configuration)
        {
            Configuration = configuration;
        }

     

        #region Default Values

       
        /// <summary>
        /// 根据Type获得对应的默认值Option
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Option<object> GetDefaultValue(Type type)
        {
            if (_defaultValues == null)
                return Option<object>.None;

            object o;
            return _defaultValues.TryGetValue(type, out o) ? Option<object>.Some(o) : Option<object>.None;
        }
        /// <summary>
        /// 设置Type对应的默认值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public void SetDefaultValue(Type type, object value)
        {
            if (_defaultValues == null)
                lock (this)
                    if (_defaultValues == null)
                        _defaultValues = new ConcurrentDictionary<Type, object>();

            _defaultValues[type] = value;
        }

        #endregion

        #region CanBeNull

      

        /// <summary>
        /// 根据Type获得是否可以为空
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Option<bool> GetCanBeNull(Type type)
        {
            if (_canBeNull == null)
                return Option<bool>.None;

            bool o;
            return _canBeNull.TryGetValue(type, out o) ? Option<bool>.Some(o) : Option<bool>.None;
        }

        /// <summary>
        /// 设置Type可以为空
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public void SetCanBeNull(Type type, bool value)
        {
            if (_canBeNull == null)
                lock (this)
                    if (_canBeNull == null)
                        _canBeNull = new ConcurrentDictionary<Type, bool>();

            _canBeNull[type] = value;
        }

        #endregion

        #region GenericConvertProvider

        volatile Dictionary<Type, List<Type[]>> _genericConvertProviders;

        public bool InitGenericConvertProvider(Type[] types, MappingSchema mappingSchema)
        {
            var changed = false;

            if (_genericConvertProviders != null)
            {
                lock (_genericConvertProviders)
                {
                    foreach (var type in _genericConvertProviders)
                    {
                        var args = type.Key.GetGenericArgumentsEx();

                        if (args.Length == types.Length)
                        {
                            if (type.Value.Aggregate(false, (cur, ts) => cur || ts.SequenceEqual(types)))
                                continue;

                            var gtype = type.Key.MakeGenericType(types);
                            var provider = (IGenericInfoProvider)Activator.CreateInstance(gtype);

                            provider.SetInfo(new MappingSchema(this));

                            type.Value.Add(types);

                            changed = true;
                        }
                    }
                }
            }

            return changed;
        }

        public void SetGenericConvertProvider(Type type)
        {
            if (_genericConvertProviders == null)
                lock (this)
                    if (_genericConvertProviders == null)
                        _genericConvertProviders = new Dictionary<Type, List<Type[]>>();

            if (!_genericConvertProviders.ContainsKey(type))
                lock (_genericConvertProviders)
                    if (!_genericConvertProviders.ContainsKey(type))
                        _genericConvertProviders[type] = new List<Type[]>();
        }

        #endregion

        #region ConvertInfo

        ConvertInfo _convertInfo;

        public void SetConvertInfo(Type from, Type to, ConvertInfo.LambdaInfo expr)
        {
            if (_convertInfo == null)
                _convertInfo = new ConvertInfo();
            _convertInfo.Set(from, to, expr);
        }

        public ConvertInfo.LambdaInfo GetConvertInfo(Type from, Type to)
        {
            return _convertInfo == null ? null : _convertInfo.Get(@from, to);
        }

        private ConcurrentDictionary<object, Func<object, object>> _converters;
        public ConcurrentDictionary<object, Func<object, object>> Converters
        {
            get { return _converters ?? (_converters = new ConcurrentDictionary<object, Func<object, object>>()); }
        }

        #endregion

        #region Scalar Types

        volatile ConcurrentDictionary<Type, bool> _scalarTypes;

        public Option<bool> GetScalarType(Type type)
        {
            if (_scalarTypes != null)
            {
                bool isScalarType;
                if (_scalarTypes.TryGetValue(type, out isScalarType))
                    return Option<bool>.Some(isScalarType);
            }

            return Option<bool>.None;
        }

        public void SetScalarType(Type type, bool isScalarType = true)
        {
            if (_scalarTypes == null)
                lock (this)
                    if (_scalarTypes == null)
                        _scalarTypes = new ConcurrentDictionary<Type, bool>();

            _scalarTypes[type] = isScalarType;
        }

        #endregion

        #region DataTypes

        volatile ConcurrentDictionary<Type, SqlDataType> _dataTypes;

        public Option<SqlDataType> GetDataType(Type type)
        {
            if (_dataTypes != null)
            {
                SqlDataType dataType;
                if (_dataTypes.TryGetValue(type, out dataType))
                    return Option<SqlDataType>.Some(dataType);
            }

            return Option<SqlDataType>.None;
        }

        public void SetDataType(Type type, DataType dataType)
        {
            SetDataType(type, new SqlDataType(dataType, type, null, null, null));
        }

        public void SetDataType(Type type, SqlDataType dataType)
        {
            if (_dataTypes == null)
                lock (this)
                    if (_dataTypes == null)
                        _dataTypes = new ConcurrentDictionary<Type, SqlDataType>();

            _dataTypes[type] = dataType;
        }

        #endregion

        #region Options

        public StringComparison? ColumnComparisonOption;

        #endregion
    }
}
