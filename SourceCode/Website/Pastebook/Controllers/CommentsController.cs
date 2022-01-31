namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;
using System.Text.Json;

public class CommentsController: Controller
{
    [HttpGet]
    [Route("/comments/{postId}")]
    public IActionResult GetCommentsByPostId(int postId)
    {
        List<CommentsModel>? commentsList = DbComments.GetCommentsByPost(postId);
        if(commentsList != null) return Json(commentsList);
        else return Ok("No Data");
        
    }

    [HttpPost]
    [Route("/comments")]
    public IActionResult AddCommentToPost(
        int postId,
        [FromBody] CommentsModel model)
    {
        string? commenterEmail = HttpContext.Request.Cookies["email"];
        model.CommenterEmail = commenterEmail;
        var jsonarr = JsonSerializer.Serialize(model,new JsonSerializerOptions{WriteIndented = true});
        System.Console.WriteLine(jsonarr);
        // model.CommenterEmail = "vincentflores88@gmail.com";
        // DbComments.AddCommentToPost(model);
        return Ok("Comment Added");

    }
}