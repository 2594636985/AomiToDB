using System;

namespace AomiToDB.Reflection
{
    /// <summary>
    /// ���󹤳�
    /// </summary>
	public interface IObjectFactory
	{
		object CreateInstance(TypeAccessor typeAccessor);
	}
}
