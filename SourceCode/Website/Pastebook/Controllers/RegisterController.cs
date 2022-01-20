namespace Controllers;
using Microsoft.AspNetCore.Mvc;

public class RegisterController : Controller
{
    [HttpGet]
    [Route("/register")]
    public IActionResult GetRegisterAction() {
        return View("/Views/Register.cshtml");
    }

    // [HttpPost]
    // [Route("/users")]
    // public IActionResult PostRegisterAction() {
    //     return View("/Views/RegisterSuccess.cshtml");
    // }
}