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
                return View("/Views/Home/Home.cshtml");
            }
        }

        return RedirectToAction("doLoginAction", "Login");
    }

    [HttpGet]
    [Route("/post")]
    public IActionResult GetPost()
    {
        return View("/Views/Post.cshtml");
    }
}