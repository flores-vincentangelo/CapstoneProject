namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

public class LoginController: Controller
{
    [Route("/login")]
    public IActionResult doLoginAction() {
        return View("/Views/Login.cshtml");
    }
}