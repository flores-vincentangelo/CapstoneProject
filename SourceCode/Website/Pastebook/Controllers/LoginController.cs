namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

public class LoginController: Controller
{
    [HttpGet]
    [Route("/login")]
    public IActionResult doLoginAction() {
        return View("./Views/Login.cshtml");
    }

    [HttpPost]
    [Route("/login")]
    public IActionResult AddSession ([FromBody] UserCredentialsModel userCredentials)
    {
        var lastLogin = (long)((System.DateTime.Now.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds);
        var readableLastLogin = new System.DateTime(1970, 1, 1).AddSeconds(lastLogin);
        var session = DbSessions.AddSessionWithCredentials(userCredentials.EmailAddress, userCredentials.Password, lastLogin);
        if (session == null) 
        {
            return Unauthorized();
        }
        else 
        {
            return (Json(session));
        }
    }

    [HttpDelete]
    [Route("/login")]
    public IActionResult DeleteSession()
    {
        string? cookieSessionId = HttpContext.Request.Cookies["sessionId"];
        DbSessions.DeleteSession(cookieSessionId);
        return RedirectToAction("doLoginAction", "Login");
    }
}