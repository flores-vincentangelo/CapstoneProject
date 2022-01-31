namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

public class HomeController: Controller
{
    [HttpGet]
    [Route("/")]
    public IActionResult GetHome()
    {
        ViewData["Title"] = "";
        string? cookieEmail = HttpContext.Request.Cookies["email"];
        string? cookieSessionId = HttpContext.Request.Cookies["sessionId"];
        string? cookieProfileLink = HttpContext.Request.Cookies["profilelink"];

        Console.WriteLine("cookieEmail: " + cookieEmail);

        if(cookieSessionId != null)
        {
            SessionsModel? sessionModel = DbSessions.GetSessionById(cookieSessionId);
            if(sessionModel != null)
            {
                var profileOwner = new ProfileModel();
                
                UserModel user = DbUsers.GetUserByEmail(cookieEmail);

                profileOwner.User = DbUsers.GetUserByEmail(cookieEmail);
                
                // Get user's posts list  
                var userPostsList = DbPosts.GetAllPostsByEmail(cookieEmail);
                var friendsPostsList = new List<PostModel>();
                var finalPostsList = new List<PostModel>();

                // Get user's friends list
                var userFriendsListString = DbFriends.GetFriendsData(cookieEmail).FriendsList;
                // Split to get individual email list
                if(userFriendsListString != null) {
                    var userFriendsList = userFriendsListString.Split(',');
                    // Console.WriteLine($"Friends count of {cookieEmail}: {userFriendsList.Length}");
                    foreach(var email in userFriendsList) {
                        // Console.WriteLine($"Friend's email: {email}"); 
                        var friendPost = DbPosts.GetAllPostsByEmail(email);
                        var count = friendPost.Count;
                        if(friendPost != null) {
                            // friendsPostsList.Concat(friendPost).ToList();
                            friendsPostsList.AddRange(friendPost);
                            // Console.WriteLine($"{email}'s total posts: {count}"); 
                        }
                    }
                    // Console.WriteLine($"Total Friend Posts count of {cookieEmail}: {friendsPostsList.Count}");
                }

                if(userPostsList != null && friendsPostsList != null) {
                    finalPostsList = userPostsList.Concat(friendsPostsList).ToList();
                    // save PostsList in descending order
                    profileOwner.PostsList = finalPostsList.OrderByDescending( item => item.DatePosted ).ToList();
                }
                else if(userPostsList == null && friendsPostsList != null) {
                    finalPostsList = friendsPostsList;
                    // save PostsList in descending order
                    profileOwner.PostsList = finalPostsList.OrderByDescending( item => item.DatePosted ).ToList();
                }
                else if(friendsPostsList == null && userPostsList != null) {
                    finalPostsList = userPostsList;
                    // save PostsList in descending order
                    profileOwner.PostsList = finalPostsList.OrderByDescending( item => item.DatePosted ).ToList();
                }
                else {
                    profileOwner.PostsList = userPostsList;
                }

                return View("~/Views/Home/Home.cshtml",profileOwner);
            }
        }

        return RedirectToAction("doLoginAction", "Login");
    }

}