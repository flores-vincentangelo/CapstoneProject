namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

public class ProfilesController: Controller
{
    // Temporary endpoint
    // Not sure if i will use it yet
    [HttpGet]
    [Route("/postlogin/{id}")]
    public IActionResult onLoginAction(string id) {
        var profile = DbProfiles.GetProfileById(id);
        return Json(profile);
    }

    // Temporary
    // For adding Profile from Postman
    [HttpPost]
    [Route("/profile")]
    public IActionResult AddProfile( [FromBody] ProfileModel profile ) {
        DbProfiles.AddProfile(profile);
        return Ok();
    }

    [HttpGet]
    [Route("/{id}")]
    public IActionResult GetProfileByLink(string id) {
        var profile = DbProfiles.GetProfileById(id);
        if(profile == null) {
            return Ok("Profile does not exist");
        }
        return View("/Views/Profile/Profile.cshtml", profile);
    }

    [HttpPatch]
    [Route("/{id}")]
    public IActionResult ModifyProfile(string id,  [FromBody] ProfileModel profile) {
        // Modify Profiles Table
        DbProfiles.ModifyProfile(id, profile);
        // Get Updated Profiles Table
        var newProfile = DbProfiles.GetProfileById(id);
        return Json(newProfile);
    }

}