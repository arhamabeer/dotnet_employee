using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_mvc.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeError(int statusCode)
        {
            Console.WriteLine("Error Controller", statusCode);
            switch (statusCode)
            {
                case 404:
                    ViewBag.Msg = "Sorry we couldn't find resources you were looking for :(";
                    break;
            }
            return View("NotFound");
        }

        [Route("Error")]
        public IActionResult GlobalExceptionHandler()
        {
            var _exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExceptionPath = _exceptionDetails.Path;
            ViewBag.ExceptionMsg = _exceptionDetails.Error.Message;
            ViewBag.ExceptionStack = _exceptionDetails.Error.StackTrace;
            return View("Error");
        }
    }
}
