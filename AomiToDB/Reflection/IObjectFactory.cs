using System;

namespace AomiToDB.Reflection
{
    /// <summary>
    /// 对象工厂
    /// </summary>
	public interface IObjectFactory
	{
		object CreateInstance(TypeAccessor typeAccessor);
	}
}
