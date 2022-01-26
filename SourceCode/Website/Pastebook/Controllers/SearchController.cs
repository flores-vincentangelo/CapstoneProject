namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

public class SearchController: Controller
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
}