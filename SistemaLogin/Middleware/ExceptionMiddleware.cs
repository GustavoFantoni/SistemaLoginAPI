using Microsoft.AspNetCore.Http;
using SistemaLogin.Errors;
using System.Net;
using System.Security.Authentication;
using System.Text.Json;

namespace SistemaLogin.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next; // Delegate: Uma das partes do ciclo de vida de uma requisição
        private readonly ILogger<ExceptionMiddleware> _logger; // Formatação no erro
        private readonly IHostEnvironment _env; // Verifica se está em produção ou desenvolvimento

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            } catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);


                // Verifica e altera o StatusCode

                var statusCode = HttpStatusCode.InternalServerError; // Status default

                if (ex is UnauthorizedAccessException)
                {
                    statusCode = HttpStatusCode.Unauthorized;
                } else if (ex is ArgumentNullException || ex is InvalidCredentialException)
                {
                    statusCode = HttpStatusCode.BadRequest;
                    
                }

                context.Response.ContentType = "application/json";

                context.Response.StatusCode = (int) statusCode;





                // Se estiver em modo desenvolvedor
                var response = _env.IsDevelopment() ?
                    new APIException(context.Response.StatusCode.ToString(), ex.Message, ex.StackTrace.ToString()) :        // Desenvolvimento
                    new APIException(context.Response.StatusCode.ToString(), ex.Message, "Ocorreu um erro no servidor!");   // Produção

                    var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                    var json =  JsonSerializer.Serialize(response, options);
                    await context.Response.WriteAsync(json);
            }
        }
    }
}
