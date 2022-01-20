namespace Controllers;
using Microsoft.AspNetCore.Mvc;

public class FriendsController: Controller
{
    [HttpGet]
    [Route("/friends")]
    public IActionResult GetHome()
    {
        ViewData["Title"] = "Friends | ";
        return View("/Views/Friends/Friends.cshtml");
    }
}