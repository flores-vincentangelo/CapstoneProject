namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

public class FriendsController: Controller
{
    [HttpGet]
    [Route("/friends")]
    public IActionResult GetFriendsPage()
    {
        ViewData["Title"] = "Friends | ";
        string? cookieEmail = HttpContext.Request.Cookies["email"];
        string? cookieSessionId = HttpContext.Request.Cookies["sessionId"];
        if(!String.IsNullOrEmpty(cookieSessionId) && !String.IsNullOrEmpty(cookieEmail))
        {
            SessionsModel? sessionModel = DbSessions.GetSessionById(cookieSessionId);
            if(sessionModel != null)
            {
                FriendsModel? friendsModel = DbFriends.GetFriendsData(cookieEmail);
                
                // UserModel userModel = 
                return View("/Views/Friends/Friends.cshtml",friendsModel);
            }
        }
        return RedirectToAction("doLoginAction", "Login");
    }

    [HttpPost]
    [Route("/friends/request")]
    public IActionResult SendFriendRequest(
        [FromBody] FriendsModel friendsModel
    )
    {
        return Ok();
    }

    [HttpPost]
    [Route("/friends/add")]
    public IActionResult AddFriend(
        [FromBody] FriendsModel friendsModel
    )
    {
        return Ok();
    } 


}