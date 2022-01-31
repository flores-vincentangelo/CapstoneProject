namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

public class LikesController: Controller
{
    [HttpPatch]
    [Route("/likes/{postId}")]
    public IActionResult LikesPost(int postId)
    {   
        //Get the likers email Address
        string emailAddress = HttpContext.Request.Cookies["email"];
        //
        string likersList = DbPosts.GetPostById(postId).LikesList;
        //Adds liker's email to Person B 
        string newLikersList = DbLikes.AddEmailtoLikesList(emailAddress, likersList);
        //Update the Database
        DbLikes.UpdateLikesListOfPost(postId, newLikersList);
        return Ok();
    }
}