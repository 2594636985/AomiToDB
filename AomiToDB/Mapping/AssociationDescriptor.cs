﻿using System;
using System.Reflection;



namespace AomiToDB.Mapping
{
    using Common;

    public class AssociationDescriptor
    {
        public AssociationDescriptor(
             Type type,
             MemberInfo memberInfo,
             string[] thisKey,
             string[] otherKey,
                       string storage,
                       bool canBeNull)
        {
            if (memberInfo == null) throw new ArgumentNullException("memberInfo");
            if (thisKey == null) throw new ArgumentNullException("thisKey");
            if (otherKey == null) throw new ArgumentNullException("otherKey");

            if (thisKey.Length == 0)
                throw new ArgumentOutOfRangeException(
                    "thisKey",
                    string.Format("Association '{0}.{1}' does not define keys.", type.Name, memberInfo.Name));

            if (thisKey.Length != otherKey.Length)
                throw new ArgumentException(
                    string.Format(
                        "Association '{0}.{1}' has different number of keys for parent and child objects.",
                        type.Name, memberInfo.Name));

            MemberInfo = memberInfo;
            ThisKey = thisKey;
            OtherKey = otherKey;
            Storage = storage;
            CanBeNull = canBeNull;
        }

        public MemberInfo MemberInfo { get; set; }
        public string[] ThisKey { get; set; }
        public string[] OtherKey { get; set; }
        public string Storage { get; set; }
        public bool CanBeNull { get; set; }

        public static string[] ParseKeys(string keys)
        {
            return keys == null ? Array<string>.Empty : keys.Replace(" ", "").Split(',');
        }
    }
}
