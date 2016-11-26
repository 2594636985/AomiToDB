using System;

namespace AomiToDB.Reflection
{
	public interface IObjectFactory
	{
		object CreateInstance(TypeAccessor typeAccessor);
	}
}
