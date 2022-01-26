namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

public class ProfilesController: Controller
{
    [HttpGet]
    [Route("/{profileLink}")]
    public IActionResult GetUserByLink(string profileLink) {
        Console.WriteLine("Profile Link: " + profileLink);
        if( profileLink != "favicon.ico" && 
            profileLink != "login" && 
            profileLink != "register" && 
            profileLink != "sessions" && 
            profileLink != "albums" && 
            profileLink != "friends")
        {
            string? cookieEmail = HttpContext.Request.Cookies["email"];
            string? cookieSessionId = HttpContext.Request.Cookies["sessionId"];
            if(cookieSessionId != null)
            {
                SessionsModel? sessionModel = DbSessions.GetSessionById(cookieSessionId);
                if(sessionModel != null)
                {
                    var user = DbUsers.GetInformationById(profileLink);
                    return View("/Views/Profile/Profile.cshtml", user);
                }
            }
            return RedirectToAction("doLoginAction", "Login");
        }
        return Ok();
    }

    [HttpPatch]
    [Route("/{profileLink}")]
    public IActionResult ModifyProfile(string profileLink,  [FromBody] UserModel user) {
        // Modify Users Table
        DbUsers.ModifyInformation(profileLink, user);
        // Get Updated User Table
        var updatedUser = DbUsers.GetInformationById(profileLink);
        return Json(updatedUser);
    }

}