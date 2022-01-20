namespace Controllers;
using Microsoft.AspNetCore.Mvc;

public class RegisterController : Controller
{
    [HttpGet]
    [Route("/register")]
    public IActionResult GetRegisterAction() {
        return View("/Views/Register.cshtml");
    }
}