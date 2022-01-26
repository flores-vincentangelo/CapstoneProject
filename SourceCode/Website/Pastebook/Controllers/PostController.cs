namespace Controllers;
using Microsoft.AspNetCore.Mvc;

public class PostController: Controller
{
    [HttpGet]
    [Route("/post")]
    public IActionResult GetPost()
    {
        return View("/Views/Post.cshtml");
    }
}