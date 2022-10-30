using CollectionManager.WEB.Models;
using CollectionsManager.BLL.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CollectionManager.WEB.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        public IActionResult Index()
            => View();

        public IActionResult Error()
        {
            var error = HttpContext.Features.Get<IExceptionHandlerFeature>().Error;

            switch (error)
            {
                case ResourceNotFoundException e:
                    return View(new ErrorViewModel { Message = e.Message });

                case ForbiddenAccessException e:
                    return View(new ErrorViewModel { Message = e.Message });

                default:
                    return View(new ErrorViewModel() { Message = $"Oops, something went wrong {error.Message}" });
            }
        }
    }
}
