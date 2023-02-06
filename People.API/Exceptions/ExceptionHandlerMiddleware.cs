namespace People.API.Exceptions;

internal sealed class ExceptionHandlerMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger)
    {
        _logger = logger;
    } 

    public async Task InvokeAsync(
        HttpContext context, 
        RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e) 
        {
            _logger.LogError(e, "Unhandeld exception occured");
            throw;
        }
    }
}
