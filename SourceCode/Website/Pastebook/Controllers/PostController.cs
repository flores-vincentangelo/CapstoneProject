namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;
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

        DbPosts.InsertPost(post);
        return Ok("Post successfully added!");
    }

    [HttpPatch]
    [Route("/posts/{postId}")]
    
    public IActionResult ModifyPost(int postId, [FromBody] PostModel post) {
        DbPosts.ModifyPost(postId, post);
        return Ok("Caption successfully modified!");
    }

    [HttpDelete]
    [Route("/posts/{postId}")]

    public IActionResult DeletePostById([FromBody] PostModel post) {
        DbPosts.DeletePostById(post.PostId);
        return Ok("Post successfully deleted!");
    }

}