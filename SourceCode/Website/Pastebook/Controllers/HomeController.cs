namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

public class HomeController: Controller
{
    [HttpGet]
    [Route("/")]
    public IActionResult GetHome()
    {
        ViewData["Title"] = "";
        string? cookieEmail = HttpContext.Request.Cookies["email"];
        string? cookieSessionId = HttpContext.Request.Cookies["sessionId"];
        if(cookieSessionId != null)
        {
            SessionsModel? sessionModel = DbSessions.GetSessionById(cookieSessionId);
            if(sessionModel != null)
            {
                UserModel user = DbUsers.GetUserByEmail(cookieEmail);
                return View("~/Views/Home/Home.cshtml",user);
            }
        }

        return RedirectToAction("doLoginAction", "Login");
    }

}