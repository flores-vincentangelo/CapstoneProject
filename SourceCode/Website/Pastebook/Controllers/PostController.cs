namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;
using System.Text.Json;
public class PostController: Controller
{
    [HttpGet]
    [Route("/posts/{postId}")]
    public IActionResult GetPostById(int postId)
    {
        string? cookieProfileLink = HttpContext.Request.Cookies["profilelink"];
        string? cookieSessionId = HttpContext.Request.Cookies["sessionId"];
        if(cookieSessionId != null)
        {
            SessionsModel? sessionModel = DbSessions.GetSessionById(cookieSessionId);
            if(sessionModel != null)
            {
                var postDetail = DbPosts.GetPostById(postId);
                var poster = DbUsers.GetUserByEmail(postDetail.EmailAddress);
                
                postDetail.Poster = poster;
                //gets comments for the post
                postDetail.CommentsListObj = DbComments.GetCommentsByPost(postDetail.PostId);
                
                return View("/Views/Posts/PostPage.cshtml", postDetail);
            }
        }
        return RedirectToAction("doLoginAction", "Login");
    }

    [HttpPost]
    [Route("/post")]
    public IActionResult doAddPost([FromBody] PostModel post) 
    {   
        string? cookieEmail = HttpContext.Request.Cookies["email"];
        string? cookieProfileLink = HttpContext.Request.Cookies["profilelink"];
        post.EmailAddress = cookieEmail;
        post.ProfileLink = cookieProfileLink;
        post.DatePosted = (long)((System.DateTime.Now.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds);

        if (post.Photo == null) {
            post.Photo = "";
            post.PhotoId = 0;
        }

        post.Likes = "";
        post.Comment = "";
        post.LikesList ="";
        post.CommentsList ="";

        DbPosts.InsertPost(post);
        return Ok("Post successfully added!");
    }

    [HttpPatch]
    [Route("/posts/{postId}")]
    
    public IActionResult ModifyPost(int postId, [FromBody] PostModel post) 
    {
        string? cookieEmail = HttpContext.Request.Cookies["email"];
        string? cookieSessionId = HttpContext.Request.Cookies["sessionId"];
        if(!String.IsNullOrEmpty(cookieSessionId) && !String.IsNullOrEmpty(cookieEmail))
        {
            SessionsModel? sessionModel = DbSessions.GetSessionById(cookieSessionId);
            if(sessionModel != null && sessionModel.EmailAddress == cookieEmail)
            {
                DbPosts.ModifyPost(postId, post);
                return Ok();
            }
        }
        return RedirectToAction("doLoginAction", "Login");
    }

    [HttpDelete]
    [Route("/posts/{postId}")]

    public IActionResult DeletePostByPostId(int postId) 
    {
        string? cookieEmail = HttpContext.Request.Cookies["email"];
        string? cookieSessionId = HttpContext.Request.Cookies["sessionId"];
        if(!String.IsNullOrEmpty(cookieSessionId) && !String.IsNullOrEmpty(cookieEmail))
        {
            SessionsModel? sessionModel = DbSessions.GetSessionById(cookieSessionId);
            if(sessionModel != null && sessionModel.EmailAddress == cookieEmail)
            {
            DbPosts.DeletePostByPostId(postId);
            return Ok();
            }
        }
        return RedirectToAction("doLoginAction", "Login");
    }

}