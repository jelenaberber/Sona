using API.Core;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult InternalServerError(this ControllerBase controller, object o)
        {
            return controller.StatusCode(500, o);
        }
    }
}
