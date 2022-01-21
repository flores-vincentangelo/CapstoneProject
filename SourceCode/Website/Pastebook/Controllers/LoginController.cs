namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

public class LoginController: Controller
{
    [HttpGet]
    [Route("/login")]
    public IActionResult doLoginAction() {
        return View("/Views/Login.cshtml");
    }

    [HttpPost]
    [Route("/")]
    public IActionResult doPostLogin()
    {
        var emailAddress = HttpContext.Request.Form["EmailAddress"];
        var model = new SessionsModel();
        model.EmailAddress = emailAddress;
        model.LastLogin = (long)((System.DateTime.Now.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds);
        DbSessions.AddSessions(model);
        return View("Views/RegisteredSuccess.cshtml");
    }
}