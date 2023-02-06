using System.Runtime.Serialization;

namespace People.Domain.Exceptions;

[Serializable]
public class DomainException : Exception
{
	public DomainException()
	{
	}

	public DomainException(string? message) : base(message) 
	{ 
	}

	public DomainException(string? message, string? paramName, Exception? innerException)
		: base(message, innerException)
	{
		ParamName = paramName;
	}

	public DomainException(string? message, string? paramName)
		: base(message)
	{
		ParamName = paramName;
	}

	public DomainException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}

	public string? ParamName { get; }
}
