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
                FriendsModel friendsModel = DbFriends.GetFriendsData(cookieEmail);
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
        string userEmail = HttpContext.Request.Cookies["email"];
        //Person B. the person who will receive the friend request
        string userToBeAdded = DbUsers.GetInformationById(profileLink).EmailAddress;
        //Person B's friend details
        FriendsModel userToBeAddedFriends = DbFriends.GetFriendsData(userToBeAdded);
        //adds person A's email to Person B's friend request list
        string newFriendReqList = DbFriends.AddEmailtoFriendRequestList(userEmail,userToBeAddedFriends.FriendRequests);
        //updates DB
        DbFriends.UpdateFriendReqsListOfUser(userToBeAdded,newFriendReqList);
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
                FriendsModel User = DbFriends.GetFriendsData(cookieEmail);
                FriendsModel UserToBeAdded = DbFriends.GetFriendsData(friendsModel.ConfirmFriendReqOf);
                

                var newFriendsListOfUser = DbFriends.AddEmailtoFriendsList(friendsModel.ConfirmFriendReqOf, User.FriendsList);
                var newFriendsReqsListOfUser = DbFriends.RemoveEmailFromFriendReqs(friendsModel.ConfirmFriendReqOf, User.FriendRequests);

                var newFriendsListOfOtherPerson = DbFriends.AddEmailtoFriendsList(cookieEmail, UserToBeAdded.FriendsList);

                DbFriends.UpdateFriendsListOfUser(User.UserEmail, newFriendsListOfUser);
                DbFriends.UpdateFriendReqsListOfUser(User.UserEmail, newFriendsReqsListOfUser);

                DbFriends.UpdateFriendsListOfUser(UserToBeAdded.UserEmail, newFriendsListOfOtherPerson);
                
                // var jsonArray2 = JsonSerializer.Serialize(User,new JsonSerializerOptions{WriteIndented = true});
                // System.Console.WriteLine(jsonArray2);
                // System.Console.WriteLine($"{Environment.NewLine} Updated friends list of user {newFriendsListOfUser} {Environment.NewLine}");
                // System.Console.WriteLine($"{Environment.NewLine} Updated friend reqs list of user {newFriendsReqsListOfUser} {Environment.NewLine}");
                // System.Console.WriteLine($"{Environment.NewLine} Updated friend reqs list of other person {newFriendsListOfOtherPerson} {Environment.NewLine}");
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
                FriendsModel user = DbFriends.GetFriendsData(cookieEmail);
                // System.Console.WriteLine($"{Environment.NewLine} {user.FriendRequests} {Environment.NewLine}");
                var newFriendsReqsListOfUser = DbFriends.RemoveEmailFromFriendReqs(friendsModel.DeleteFriendReqOf, user.FriendRequests);
                // System.Console.WriteLine($"{Environment.NewLine} {newFriendsReqsListOfUser} {Environment.NewLine}");
                DbFriends.UpdateFriendReqsListOfUser(user.UserEmail, newFriendsReqsListOfUser);
                // System.Console.WriteLine($"{Environment.NewLine} {newFriendsReqsListOfUser} {Environment.NewLine}");
                return Ok();
            }
        }


        return RedirectToAction("doLoginAction", "Login");
    }
}