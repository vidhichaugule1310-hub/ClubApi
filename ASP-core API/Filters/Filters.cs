using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace ClubApi.Filters
{
    public class TimingFilter : IActionFilter
    {
        private readonly ILogger<TimingFilter> _log;
        public TimingFilter(ILogger<TimingFilter> log) => _log = log;

        public void OnActionExecuting(ActionExecutingContext c) =>
            c.HttpContext.Items["sw"] = Stopwatch.StartNew();

        public void OnActionExecuted(ActionExecutedContext c)
        {
            var sw = (Stopwatch)c.HttpContext.Items["sw"]!;
            _log.LogInformation("{A} took {Ms} ms", c.ActionDescriptor.DisplayName, sw.ElapsedMilliseconds);
        }
    }
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext c)
        {
            c.Result = new ObjectResult(new { error = c.Exception.Message }) { StatusCode = 500 };
            c.ExceptionHandled = true;
        }
    }
    public class ApiKeyFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext c)
        {
            if (!c.HttpContext.Request.Headers.TryGetValue("X-Api-Key", out var k) || k != "secret123")
                c.Result = new UnauthorizedResult();
        }
    }
    public class PoweredByFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext c) =>
            c.HttpContext.Response.Headers["X-Club"] = "ClubApi";
        public void OnResultExecuted(ResultExecutedContext c) { }
    }
}   
