using System;

// ReSharper disable CheckNamespace

namespace AomiToDB
{
	partial class Sql
	{
		[AttributeUsage(AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
		public class EnumAttribute : Attribute
		{
		}
	}
}
