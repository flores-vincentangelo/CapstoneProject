namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

public class NotificationsController: Controller
{
    // [HttpPatch]
    // [Route("/notifications")]
    // public IActionResult

    [HttpGet]
    [Route("/notifications")]
    public IActionResult GetNotifications()
    {
        string? cookieEmail = HttpContext.Request.Cookies["email"];
        Dictionary<string, string> notifObj = DbNotifications.GetNotificationsByEmail("vincentflores88@gmail.com");
        NotificationsModel notifModel = new NotificationsModel();
        notifModel.FriendReq = DbNotifications.GetUserListByEmail(notifObj["FriendRequests"]);
        notifModel.Likers = DbNotifications.GetUserListByEmail(notifObj["Likers"]);
        notifModel.Commenters = DbNotifications.GetUserListByEmail(notifObj["Commenters"]);
        return Json(notifModel);
    }
}