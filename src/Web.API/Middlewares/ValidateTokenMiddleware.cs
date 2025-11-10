using System.Net;
using System.Text.Json;

namespace Web.API.Middlewares;

public class ValidateTokenMiddleware : IMiddleware
{
	private readonly ILogger<ValidateTokenMiddleware> _logger;
    
    private readonly string _token = "rYejThPK9G1NEAP4lKsiCp5e2dn9";

	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
		try
		{
            var token = context.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            if (!token.Equals(_token))
			{
				throw new UnauthorizedAccessException();
            }

			await next(context);
		}
		catch (UnauthorizedAccessException)
		{
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            ProblemDetails problem = new()
            {
                Status = (int)HttpStatusCode.Unauthorized,
                Type = "No autorizado",
                Title = "No autorizado",
                Detail = "Favor de validar token."
            };

            string json = JsonSerializer.Serialize(problem);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(json);
        }
    }
}
