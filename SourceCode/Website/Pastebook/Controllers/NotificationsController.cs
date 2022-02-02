namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

public class NotificationsController: Controller
{

    [HttpGet]
    [Route("/notifications")]
    public IActionResult GetNotifications()
    {
        string? cookieEmail = HttpContext.Request.Cookies["email"];
        int loggedInUserId = DbUsers.GetUserByEmail(cookieEmail).UserId;
        Dictionary<string, string> notifObj = DbNotifications.GetNotificationsByUserId(loggedInUserId);
        NotificationsModel notifModel = new NotificationsModel();
        notifModel.FriendReq = DbNotifications.GetUserListByUserId(notifObj["FriendRequests"]);
        notifModel.Likers = DbNotifications.GetUserListByUserId(notifObj["Likers"]);
        notifModel.Commenters = DbNotifications.GetUserListByUserId(notifObj["Commenters"]);
        return Json(notifModel);
    }

    [HttpDelete]
    [Route("/notifications")]
    public IActionResult DeleteNotifications()
    {
        string? cookieEmail = HttpContext.Request.Cookies["email"];
        int loggedInUserId = DbUsers.GetUserByEmail(cookieEmail).UserId;
        DbNotifications.DeleteNotificationsByUserId(loggedInUserId);
        return Ok();
    }
}