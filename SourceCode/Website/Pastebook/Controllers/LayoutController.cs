namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

public class LayoutController: Controller
{
    [HttpGet]
    [Route("/search/{searchTerm}")]
    public IActionResult SearchUser(string? searchTerm)
    {
        List<UserModel>? userList = new List<UserModel>();
        if(String.IsNullOrEmpty(searchTerm))
        {
            userList = null;
            return Json(userList);
        }
        else
        {
            userList = DbUsers.GetUserByFirstOrLastName(searchTerm);
            if(userList.Count == 0)
            {
                return Json(userList);
            }
            return Json(userList);
        }
    }

    [HttpGet]
    [Route("/user")]
    public IActionResult GetUserDetails()
    {
        string? cookieEmail = HttpContext.Request.Cookies["email"];
        var user = DbUsers.GetUserByEmail(cookieEmail);
        return (Json(user));
    }
}