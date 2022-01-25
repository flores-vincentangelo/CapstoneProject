namespace Controllers;
using Microsoft.AspNetCore.Mvc;

public class HomeController: Controller
{
    [HttpGet]
    [Route("/")]
    public IActionResult GetHome()
    {
        ViewData["Title"] = "";
        return View("/Views/Home/Home.cshtml");
    }

    [HttpGet]
    [Route("/post")]
    public IActionResult GetPost()
    {
        return View("/Views/Post.cshtml");
    }
}