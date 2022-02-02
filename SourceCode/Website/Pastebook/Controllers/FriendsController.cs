namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;
using System.Text.Json;

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
                int userId = DbUsers.GetUserByEmail(cookieEmail).UserId;
                FriendsModel friendsModel = DbFriends.GetFriendsData(userId);
                friendsModel.FriendRequestObjList = DbFriends.GetListAsUserObj(friendsModel.FriendRequests);
                friendsModel.FriendsObjList = DbFriends.GetListAsUserObj(friendsModel.FriendsList);
                return View("/Views/Friends/Friends.cshtml",friendsModel);
            }
        }
        return RedirectToAction("doLoginAction", "Login");
    }

    [HttpPatch]
    [Route("/friends/request/{profileLink}")]
    public IActionResult SendFriendRequest(string profileLink)
    {   
        //Person A. The person who send the friend request
        string sentFriendReq = HttpContext.Request.Cookies["email"];
        int sentFriendReqId = DbUsers.GetUserByEmail(sentFriendReq).UserId;
        //Person B. the person who will receive the friend request
        int receiveFriendReqId = DbUsers.GetInformationById(profileLink).UserId;
        //Person B's friend details
        FriendsModel receiveFriendReqFriendsData = DbFriends.GetFriendsData(receiveFriendReqId);
        //adds person A's email to Person B's friend request list
        string newFriendReqList = DbFriends.AddUserIdToFriendReqList(sentFriendReqId,receiveFriendReqFriendsData.FriendRequests);
        //updates DB
        DbFriends.UpdateFriendReqsListOfUser(receiveFriendReqId,newFriendReqList);
        //gives notif to Person B
        DbNotifications.InsertUserIntoFriendReqNotifOfOtherUser(sentFriendReqId, receiveFriendReqId);
        return Ok();
    }

    [HttpPatch]
    [Route("/friends/confirm")]
    public IActionResult AddFriend(
        [FromBody] FriendsModel friendsModel
    )
    {
        string? cookieEmail = HttpContext.Request.Cookies["email"];
        string? cookieSessionId = HttpContext.Request.Cookies["sessionId"];
        if(!String.IsNullOrEmpty(cookieSessionId) && !String.IsNullOrEmpty(cookieEmail))
        {
            SessionsModel? sessionModel = DbSessions.GetSessionById(cookieSessionId);
            if(sessionModel != null && sessionModel.EmailAddress == cookieEmail)
            {
                int loggedInUserId = DbUsers.GetUserByEmail(cookieEmail).UserId;
                FriendsModel User = DbFriends.GetFriendsData(loggedInUserId);
                int userToBeAddedId = DbUsers.GetUserByEmail(friendsModel.ConfirmFriendReqOf).UserId;
                FriendsModel UserToBeAdded = DbFriends.GetFriendsData(userToBeAddedId);
                

                var newFriendsListOfUser = DbFriends.AddUserIdToFriendsList(userToBeAddedId, User.FriendsList);
                var newFriendsReqsListOfUser = DbFriends.RemoveIdFromFriendReqs(userToBeAddedId, User.FriendRequests);

                var newFriendsListOfOtherPerson = DbFriends.AddUserIdToFriendsList(loggedInUserId, UserToBeAdded.FriendsList);

                DbFriends.UpdateFriendsListOfUser(loggedInUserId, newFriendsListOfUser);
                DbFriends.UpdateFriendReqsListOfUser(loggedInUserId, newFriendsReqsListOfUser);

                DbFriends.UpdateFriendsListOfUser(userToBeAddedId, newFriendsListOfOtherPerson);
                
                return Ok();
            }
        }

        return RedirectToAction("doLoginAction", "Login");
    } 

    [HttpPatch]
    [Route("/friends/delete")]
    public IActionResult DeleteRequest(
        [FromBody] FriendsModel friendsModel
    )
    {
        string? cookieEmail = HttpContext.Request.Cookies["email"];
        string? cookieSessionId = HttpContext.Request.Cookies["sessionId"];
        if(!String.IsNullOrEmpty(cookieSessionId) && !String.IsNullOrEmpty(cookieEmail))
        {
            SessionsModel? sessionModel = DbSessions.GetSessionById(cookieSessionId);
            if(sessionModel != null && sessionModel.EmailAddress == cookieEmail)
            {
                int loggedInUserId = DbUsers.GetUserByEmail(cookieEmail).UserId;
                FriendsModel user = DbFriends.GetFriendsData(loggedInUserId);
                
                int senderOfFriendReqId = DbUsers.GetUserByEmail(friendsModel.DeleteFriendReqOf).UserId;
                var newFriendsReqsListOfUser = DbFriends.RemoveIdFromFriendReqs(senderOfFriendReqId, user.FriendRequests);
                
                DbFriends.UpdateFriendReqsListOfUser(loggedInUserId, newFriendsReqsListOfUser);
        
                return Ok();
            }
        }
        return RedirectToAction("doLoginAction", "Login");
    }
}