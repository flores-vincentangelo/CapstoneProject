namespace Controllers;
using Microsoft.AspNetCore.Mvc; 
using Database;
using Models;

public class SettingsController : Controller
{
    [HttpGet]
    [Route("/settings/{id}")]
    public IActionResult GetInfoById(string id) {
        var user = DbUsers.GetInformationById(id);
        return View("/Views/Settings/Settings.cshtml", user);
    }

    [HttpPatch]
    [Route("/settings/{id}")]
    public IActionResult ModifyInformation(string id,  [FromBody] UserModel user) {
        // Modify Users Table
        DbUsers.ModifyInformation(id, user);
        // Get Updated Users Table
        var newUser = DbUsers.GetInformationById(id);
        return Json(newUser);
    }
}