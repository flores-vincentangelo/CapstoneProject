namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;
public class PostController: Controller
{
    [HttpGet]
    [Route("/postX")]
    public IActionResult GetPost()
    {
        return View("/Views/Post.cshtml");
    }

    [HttpGet]
    [Route("/post")]
    public IActionResult GetAllPostDetails() {
        string? cookieProfileLink = HttpContext.Request.Cookies["profilelink"];
        string? cookieSessionId = HttpContext.Request.Cookies["sessionId"];
        if(cookieSessionId != null)
        {
            SessionsModel? sessionModel = DbSessions.GetSessionById(cookieSessionId);
            if(sessionModel != null)
            {
                var posts = DbPosts.GetAllPostDetails(cookieProfileLink);
                if(posts == null) {
                    return Ok("No Post found");
                }
                return Json(posts);
            }
        }
        return Ok();
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

        DbPosts.InsertPost(post);
        return Ok("Post successfully added!");
    }
}