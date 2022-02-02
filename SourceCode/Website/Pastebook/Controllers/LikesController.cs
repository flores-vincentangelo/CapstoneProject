namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

public class LikesController: Controller
{

    [HttpGet]
    [Route("/likes/{postId}")]
    public IActionResult GetLikers(int postId)
    { 
        var list = DbPosts.GetPostById(postId).LikesList;
        List<UserModel> likersList = DbLikes.GetListAsUserObj(list);
        return Json(likersList);
    }

    [HttpPatch]
    [Route("/likes/{postId}")]
    public IActionResult LikesPost(int postId)
    {   
        //Liker's Information
        string likedAPost = HttpContext.Request.Cookies["email"];
        int likedAPostId = DbUsers.GetUserByEmail(likedAPost).UserId;
        //
        string likersList = DbPosts.GetPostById(postId).LikesList;
        //Adds liker's User Id to Likers List
        string newLikersList = DbLikes.AddUserIdtoLikesList(likedAPostId, likersList);
        //Update the Database
        DbLikes.UpdateLikesListOfPost(postId, newLikersList);

        //user whose post has been liked. Person Liked
        int? likeRecipientId = DbPosts.GetPostById(postId).UserId;
        //gives notification to person Liked
        DbNotifications.InsertUserIntoLikesNotifOfOtherUser(likedAPostId, likeRecipientId);
        return Ok();
    }

    [HttpPatch]
    [Route("/unlike/{postId}")]
    public IActionResult UnlikesPost(int postId)
    { 
        //Removed like
        string unlikedAPost = HttpContext.Request.Cookies["email"];
        int unlikedAPostId = DbUsers.GetUserByEmail(unlikedAPost).UserId;
        //
        string likersList = DbPosts.GetPostById(postId).LikesList;
        //Delete liker's User Id
        string newLikersList = DbLikes.RemoveUserIdFromLikesList(unlikedAPostId, likersList);
        //Update the Database
        DbLikes.UpdateLikesListOfPost(postId, newLikersList);
        return Ok();
    }
}