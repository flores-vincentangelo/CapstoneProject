namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

public class ProfilesController: Controller
{
    [HttpGet]
    [Route("/{profileLink}")]
    public IActionResult GetUserByLink(string profileLink) {
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
    
    [HttpPatch]
    [Route("/{profileLink}")]
    public IActionResult ModifyProfile(string profileLink,  [FromBody] UserModel user) {
        
        // Get Updated User Table
        var updatedUser = DbUsers.GetInformationById(profileLink);

        // Modify Users Table
        if (!String.IsNullOrEmpty(user.EmailAddress)) {
            bool result = false;
            result = BCrypt.Net.BCrypt.Verify(user.Password, updatedUser.Password);
            if(result)
            {
                DbUsers.ModifyInformation(profileLink, user);
                updatedUser = DbUsers.GetInformationById(profileLink);
                return Json(updatedUser);
            }
            else 
            {
                return(Unauthorized());
            }
        }
        else if (!String.IsNullOrEmpty(user.Password)) {
            bool result = false;
            result = BCrypt.Net.BCrypt.Verify(user.Password, updatedUser.Password);
            if(result)
            {
                user.Password = user.NewPassword;
                DbUsers.ModifyInformation(profileLink, user);
                updatedUser = DbUsers.GetInformationById(profileLink);
                return Json(updatedUser);
            }
            else 
            {
                return(Unauthorized());
            }
        }
        else 
        { 
            DbUsers.ModifyInformation(profileLink, user);
            updatedUser = DbUsers.GetInformationById(profileLink);
            return Json(updatedUser);
        }
    }
}