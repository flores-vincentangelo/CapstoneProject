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
        string? cookieProfileLink = HttpContext.Request.Cookies["profilelink"];
        if(cookieSessionId != null)
        {
            SessionsModel? sessionModel = DbSessions.GetSessionById(cookieSessionId);
            if(sessionModel != null)
            {
                var profileOwner = new ProfileModel();
                
                UserModel user = DbUsers.GetUserByEmail(cookieEmail);
                profileOwner.User = DbUsers.GetUserByEmail(cookieEmail);
                profileOwner.PostsList = DbPosts.GetAllPostDetails(cookieProfileLink);

                return View("/Views/Home/Home.cshtml",profileOwner);
            }
        }

        return RedirectToAction("doLoginAction", "Login");
    }

}