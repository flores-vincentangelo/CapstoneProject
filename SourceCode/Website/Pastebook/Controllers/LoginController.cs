namespace Controllers;
using Microsoft.AspNetCore.Mvc;

public class LoginController : Controller
{
    [HttpGet]
    // [Route("/")]
    public IActionResult GetLoginAction() {
        return View("/Views/Login.cshtml");
    }
}