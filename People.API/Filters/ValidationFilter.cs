using FluentValidation;
using System.Net;
using System.Reflection;

namespace People.API.Filters;

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
public class ValidateAttribute : Attribute {}

public class ValidationFilter
{
    public static EndpointFilterDelegate ValidationFilterFactory(
        EndpointFilterFactoryContext context,
        EndpointFilterDelegate next)
    {
        if (HasValidationAttribute(context.MethodInfo))
        {
            return invocationContext => Validate(
               context,
               invocationContext,
               next);
        }

        // pass-thru
        return invocationContext => next(invocationContext);
    }

    private static bool HasValidationAttribute(MethodInfo methodInfo)
        => methodInfo.GetParameters()
            .Any(x => x.GetCustomAttribute<ValidateAttribute>() is not null);

    private static async ValueTask<object?> Validate(
        EndpointFilterFactoryContext context,
        EndpointFilterInvocationContext invocationContext, 
        EndpointFilterDelegate next)
    {
        IEnumerable<ValidationDescriptor> validationDescriptors = GetValidators(
            context.MethodInfo, invocationContext.HttpContext);

        foreach (ValidationDescriptor descriptor in validationDescriptors)
        {
            var argument = invocationContext.Arguments[descriptor.ArgumentIndex];

            if (argument is not null)
            {
                var validationResult = await descriptor.Validator.ValidateAsync(
                    new ValidationContext<object>(argument)
                );

                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary(),
                        statusCode: (int)HttpStatusCode.UnprocessableEntity);
                }
            }
        }

        return await next.Invoke(invocationContext);
    }

    static IEnumerable<ValidationDescriptor> GetValidators(
        MethodInfo methodInfo, 
        HttpContext httpContext)
    {
        ParameterInfo[] parameters = methodInfo.GetParameters();

        for (int i = 0; i < parameters.Length; i++)
        {
            ParameterInfo parameter = parameters[i];

            if (parameter.GetCustomAttribute<ValidateAttribute>() is not null)
            {
                Type validatorType = typeof(IValidator<>).MakeGenericType(parameter.ParameterType);

                IValidator? validator = httpContext.RequestServices
                    .GetService(validatorType) as IValidator;

                if (validator is not null)
                {
                    yield return new ValidationDescriptor 
                    { 
                        ArgumentIndex = i, 
                        ArgumentType = parameter.ParameterType, 
                        Validator = validator 
                    };
                }
            }
        }
    }

    private class ValidationDescriptor
    {
        public required int ArgumentIndex { get; init; }
        public required Type ArgumentType { get; init; }
        public required IValidator Validator { get; init; }
    }
}
