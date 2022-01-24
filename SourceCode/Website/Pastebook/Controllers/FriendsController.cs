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
                // FriendsModel model = DbFriends.GetFriendsData(cookieEmail);
                FriendsModel model = new FriendsModel();
                model.UserEmail = cookieEmail;
                model.FriendsList = "a@gmail.com,b@gmail.com,c@gmail.com,d@gmail.com,e@gmail.com";
                model.FriendRequests = "a@gmail.com,b@gmail.com,c@gmail.com,d@gmail.com,e@gmail.com";
                return View("/Views/Friends/Friends.cshtml",model);
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