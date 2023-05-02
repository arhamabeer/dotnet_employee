using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_mvc.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeError(int statusCode)
        {
            Console.WriteLine("Error Controller", statusCode);
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.Msg = "Sorry we couldn't find resources you were looking for :(";
                    break;
            }
            _logger.LogWarning($"The {statusCode} Error occured. We cannot find the requested ID in our Database. Error Path is {statusCodeResult.OriginalPath} and Query String is {statusCodeResult.OriginalQueryString}");
            return View("NotFound");
        }

        [Route("Error")]
        public IActionResult GlobalExceptionHandler()
        {
            var _exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _logger.LogError($"The Error is occured at {_exceptionDetails.Path}, with a message " +
                $"{_exceptionDetails.Error.Message} and having stack trace: " +
                $"{_exceptionDetails.Error.StackTrace}");
            ViewBag.ExceptionPath = _exceptionDetails.Path;
            return View("Error");
        }
    }
}
