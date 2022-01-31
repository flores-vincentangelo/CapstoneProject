namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

public class MainController: Controller
{

    [HttpGet]
    [Route("/sample/{id}")]
    public IActionResult GetSample(int id)
    {
        UserModel user = DbUsers.GetUserById(id);
        return Json(user);
    }
}