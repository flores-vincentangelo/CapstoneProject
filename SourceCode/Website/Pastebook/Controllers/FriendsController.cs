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
                List<UserModel> friendRequestsObj = new List<UserModel>();
                List<UserModel> friendsListObj = new List<UserModel>();
                if(String.IsNullOrEmpty(friendsModel.FriendRequests))
                {
                    System.Console.WriteLine($"{Environment.NewLine} null {Environment.NewLine}");
                    friendsModel.FriendRequestObjList = null;
                }
                else
                {
                    System.Console.WriteLine($"{Environment.NewLine} {friendsModel.FriendRequests} {Environment.NewLine}");
                    var emailArr = friendsModel.FriendRequests.Split(",");
                    foreach (string email in emailArr)
                    {
                        UserModel userModel = DbUsers.GetUserByEmail(email);
                        friendRequestsObj.Add(userModel);
                    }

                    friendsModel.FriendRequestObjList = friendRequestsObj;
                }

                if(String.IsNullOrEmpty(friendsModel.FriendsList))
                {
                    friendsModel.FriendsObjList = null;
                }
                else
                {
                    
                }
                
                // UserModel userModel = 
                return View("/Views/Friends/Friends.cshtml",friendsModel);
            }
        }
        return RedirectToAction("doLoginAction", "Login");
    }

    [HttpPatch]
    [Route("/friends/request")]
    public IActionResult SendFriendRequest(
        [FromBody] FriendsModel friendsModel
    )
    {
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
                friendsModel.UserEmail = cookieEmail;
                FriendsModel friendsModel2 = DbFriends.GetFriendsData(cookieEmail);
                
                string? friendReqStr = friendsModel2.FriendRequests;
                string? friendListStr = friendsModel2.FriendsList;

                List<string> friendReqList = new List<string>();
                List<string> friendsList = new List<string>();

                if(!String.IsNullOrEmpty(friendReqStr))
                {
                    var _friendReqArr = friendReqStr.Split(",");
                    friendReqList = new List<string>(_friendReqArr);
                    System.Console.WriteLine($"{Environment.NewLine} Friend Requests Before Remove: {String.Join(",",friendReqList)} {Environment.NewLine}");
                    friendReqList.Remove(friendsModel.ConfirmFriendReqOf);
                    
                }
                else
                {
                    //fix this, a user cannot accept a friend request if s/he has no friend requests to begin with
                    return Ok();
                }

                System.Console.WriteLine($"{Environment.NewLine} Friends List Before Add: {friendListStr} {Environment.NewLine}");

                if(!String.IsNullOrEmpty(friendListStr))
                {
                    var _friendsListArr = friendListStr.Split(",");
                    friendsList = new List<string>(_friendsListArr);
                    friendsList.Add(friendsModel.ConfirmFriendReqOf);
                }
                else
                {
                    friendsList.Add(friendsModel.ConfirmFriendReqOf);
                }

                friendReqStr = String.Join(",",friendReqList);
                friendListStr = String.Join(",",friendsList);

                System.Console.WriteLine($"{Environment.NewLine} Friend Requests After Remove: {friendReqStr} {Environment.NewLine}");
                System.Console.WriteLine($"{Environment.NewLine} Friends List After Remove: {friendListStr} {Environment.NewLine}");
                
                
                // System.Console.WriteLine($"{Environment.NewLine} Friend Requests {String.Join(",",friendReqList)} {Environment.NewLine}");







                // var jsonArray2 = JsonSerializer.Serialize(friendsModel2,new JsonSerializerOptions{WriteIndented = true});
                // System.Console.WriteLine(jsonArray2);
                return Ok();
            }
        }

        return RedirectToAction("doLoginAction", "Login");
    } 


}