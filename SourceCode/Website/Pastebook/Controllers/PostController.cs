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
        ViewData["Title"] = "Post | ";
        string? cookieEmail = HttpContext.Request.Cookies["email"];
        string? cookieProfileLink = HttpContext.Request.Cookies["profilelink"];
        string? cookieSessionId = HttpContext.Request.Cookies["sessionId"];
        if(cookieSessionId != null)
        {
            SessionsModel? sessionModel = DbSessions.GetSessionById(cookieSessionId);
            if(sessionModel != null)
            {
                int loggedInUserId = DbUsers.GetUserByEmail(cookieEmail).UserId;
                var postDetail = DbPosts.GetPostById(postId);
                postDetail.Poster = DbUsers.GetUserById(postDetail.UserId);
                //gets comments for the post
                postDetail.CommentsListObj = DbComments.GetCommentsByPost(postDetail.PostId);
                
                postDetail.DoesUserLikesAPost = DbLikes.IsUserInLikersList(loggedInUserId, postDetail.LikesList);
                postDetail.DoesUserOwnsThePost = postDetail.UserId == loggedInUserId ? true : false;
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
        post.UserId = DbUsers.GetUserByEmail(cookieEmail).UserId;
        post.ProfileLink = cookieProfileLink;
        post.DatePosted = (long)((System.DateTime.Now.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds);

        if (post.Photo == null) {
            post.Photo = "";
            post.PhotoId = 0;
        }

        post.LikesList = "";

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