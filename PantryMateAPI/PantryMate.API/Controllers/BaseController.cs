using Microsoft.AspNetCore.Mvc;
using PantryMate.API.Entities;

namespace PantryMate.API.Controllers
{
    [Controller]
    public abstract class BaseController : ControllerBase
    {
        public Account Account => (Account)HttpContext.Items["Account"];
    }
}
