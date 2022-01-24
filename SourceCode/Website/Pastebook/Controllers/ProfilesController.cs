namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

public class ProfilesController: Controller
{
    [HttpGet]
    [Route("/{profileLink}")]
    public IActionResult GetUserByLink(string profileLink) {
        var user = DbUsers.GetInformationById(profileLink);
        if(user == null) {
            return Ok("Profile does not exist");
        }
        return View("/Views/Profile/Profile.cshtml", user);
    }

    [HttpPatch]
    [Route("/{profileLink}")]
    public IActionResult ModifyProfile(string profileLink,  [FromBody] UserModel user) {
        // Modify Users Table
        DbUsers.ModifyInformation(profileLink, user);
        // Get Updated User Table
        var updatedUser = DbUsers.GetInformationById(profileLink);
        return Json(updatedUser);
    }

}