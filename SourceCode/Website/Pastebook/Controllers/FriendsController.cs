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
                FriendsModel User = DbFriends.GetFriendsData(cookieEmail);
                FriendsModel UserToBeAdded = DbFriends.GetFriendsData(friendsModel.ConfirmFriendReqOf);
                

                var newFriendsListOfUser = DbFriends.AddEmailtoFriendsList(friendsModel.ConfirmFriendReqOf, User.FriendsList);
                var newFriendsReqsListOfUser = DbFriends.RemoveEmailFromFriendReqs(friendsModel.ConfirmFriendReqOf, User.FriendRequests);

                var newFriendsListOfOtherPerson = DbFriends.AddEmailtoFriendsList(cookieEmail, UserToBeAdded.FriendsList);
                
                var jsonArray2 = JsonSerializer.Serialize(User,new JsonSerializerOptions{WriteIndented = true});
                System.Console.WriteLine(jsonArray2);
                System.Console.WriteLine($"{Environment.NewLine} Updated friends list of user {newFriendsListOfUser} {Environment.NewLine}");
                System.Console.WriteLine($"{Environment.NewLine} Updated friend reqs list of user {newFriendsReqsListOfUser} {Environment.NewLine}");
                System.Console.WriteLine($"{Environment.NewLine} Updated friend reqs list of other person {newFriendsListOfOtherPerson} {Environment.NewLine}");
                return Ok();
            }
        }

        return RedirectToAction("doLoginAction", "Login");
    } 


}