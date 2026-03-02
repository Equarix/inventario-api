using invetario_api.Exceptions;
using invetario_api.utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace invetario_api.Filters
{
    public class GlobalExceptionFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var statusCode = context.Exception switch
            {
                HttpException httpException => httpException.StatusCode,
                _ => 500
            };


            Console.WriteLine(context.Exception);

            var message = context.Exception.Message;

            context.Result = new ObjectResult(
                ResponseApi<Object>.NotFound(statusCode, message))
            {
                StatusCode = statusCode
            };

            context.ExceptionHandled = true;
            await Task.CompletedTask;
        }
    }
}
