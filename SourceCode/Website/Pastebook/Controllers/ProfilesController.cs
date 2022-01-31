namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;
using System.Text.Json;

public class ProfilesController: Controller
{
    [HttpGet]
    [Route("/{profileLink}")]
    public IActionResult GetUserByLink(string profileLink) {
        
        bool doesProfileExist = DbUsers.DoesProfileExist(profileLink);
        if(doesProfileExist)
        {
            string? cookieEmail = HttpContext.Request.Cookies["email"];
            string? cookieSessionId = HttpContext.Request.Cookies["sessionId"];
            if(cookieSessionId != null)
            {
                SessionsModel? sessionModel = DbSessions.GetSessionById(cookieSessionId);
                if(sessionModel != null)
                {

                    var profileOwnerDetails = DbUsers.GetInformationById(profileLink);
                    var profileOwner = new ProfileModel();
                    profileOwner.DoesUserOwnProfile = DbUsers.DoesUserOwnProfile(cookieEmail,profileLink);
                    profileOwner.User = profileOwnerDetails;
                    profileOwner.AlbumList = DbAlbums.GetAllAlbumsByProfileLink(profileLink);
                    profileOwner.PhotoList = DbPhotos.GetAllPhotosByProfileLink(profileLink);
                    
                    FriendsModel profileOwnerFriends = DbFriends.GetFriendsData(profileOwner.User.UserId);
                    profileOwner.IsUserInFriendsList = DbFriends.IsInFriendsList(cookieEmail,profileOwnerFriends.FriendsList);
                    profileOwner.IsUserInFriendReqList = DbFriends.IsInFriendReqList(cookieEmail, profileOwnerFriends.FriendRequests);
                    FriendsModel userFriendsData = DbFriends.GetFriendsData(cookieEmail);
                    profileOwner.IsProfileOwnerInFriendReqList = DbFriends.IsInFriendReqList(profileOwner.User.EmailAddress,userFriendsData.FriendRequests);
                    profileOwner.FriendsList = DbFriends.GetListAsUserObj(profileOwnerFriends.FriendsList);

                    profileOwner.PostsList = DbPosts.GetAllPostDetails(profileLink);
                    
                    return View("/Views/Profile/Profile.cshtml", profileOwner);
                }
            }
            return RedirectToAction("doLoginAction", "Login");
        }
        return Ok();
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
        else if (!String.IsNullOrEmpty(user.ReadableBirthday)) {

            DateTime birthDate = DateTime.Parse(user.ReadableBirthday);
            var dateOfBirth = (long)((birthDate.Subtract(new System.DateTime(1970, 1, 1, 0, 0, 0, 0))).TotalSeconds);
            user.Birthday = dateOfBirth;
            DbUsers.ModifyInformation(profileLink, user);
            updatedUser = DbUsers.GetInformationById(profileLink);
            return Json(updatedUser);
        }
        else 
        { 
            DbUsers.ModifyInformation(profileLink, user);
            updatedUser = DbUsers.GetInformationById(profileLink);
            return Json(updatedUser);
        }
    }
}