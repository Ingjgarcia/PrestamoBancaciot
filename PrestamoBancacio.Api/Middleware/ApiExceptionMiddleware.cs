namespace PrestamoBancacio.Api.Middleware
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionMiddleware> _logger;
        public ApiExceptionMiddleware(RequestDelegate next, ILogger<ApiExceptionMiddleware> logger)
        { _next = next; _logger = logger; }

        public async Task Invoke(HttpContext ctx)
        {
            try { await _next(ctx); }
            catch (KeyNotFoundException ex) { await Write(ctx, 404, ex.Message); }
            catch (ArgumentException ex) { await Write(ctx, 400, ex.Message); }
            catch (AccessViolationException ex) { await Write(ctx, 403, ex.Message); }
            catch (InvalidOperationException ex) { await Write(ctx, 409, ex.Message); }
            catch (UnauthorizedAccessException ex) { await Write(ctx, 403, "Acceso denegado: solo administradores pueden acceder"); }
            catch (Exception ex) { _logger.LogError(ex, "error no manejado"); await Write(ctx, 500, "Error interno del servidor"); }
        }

        private static async Task Write(HttpContext ctx, int status, string message)
        {
            ctx.Response.StatusCode = status; ctx.Response.ContentType = "application/json";
            await ctx.Response.WriteAsJsonAsync(new { message });
        }

       

    }

}
