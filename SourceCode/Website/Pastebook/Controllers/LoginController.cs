namespace Controllers;
using Microsoft.AspNetCore.Mvc;

public class LoginController: Controller
{
    [Route("/login")]
    public IActionResult doLoginAction() {
        return View("/Views/Login.cshtml");
    }
}