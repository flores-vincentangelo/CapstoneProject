namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

public class FriendsController: Controller
{
    [HttpGet]
    [Route("/friends")]
    public IActionResult GetHome()
    {
        ViewData["Title"] = "Friends | ";
        return View("/Views/Friends/Friends.cshtml");
    }

    [HttpPost]
    [Route("/friends/request")]
    public IActionResult SendFriendRequest(
        [FromBody] FriendsModel friendsModel
    )
    {
        return Ok();
    }

    [HttpPost]
    [Route("/friends/add")]
    public IActionResult AddFriend(
        [FromBody] FriendsModel friendsModel
    )
    {
        return Ok();
    } 


}