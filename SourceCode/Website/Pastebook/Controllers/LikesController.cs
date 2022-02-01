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
        //Liker
        string likedAPost = HttpContext.Request.Cookies["email"];
        int likedAPostId = DbUsers.GetUserByEmail(likedAPost).UserId;
        //
        string likersList = DbPosts.GetPostById(postId).LikesList;
        //Adds liker's User Id to Person B 
        string newLikersList = DbLikes.AddUserIdtoLikesList(likedAPostId, likersList);
        //Update the Database
        DbLikes.UpdateLikesListOfPost(postId, newLikersList);
        return Ok();
    }
}