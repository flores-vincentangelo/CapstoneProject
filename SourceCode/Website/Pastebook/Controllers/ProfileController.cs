namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

public class ProfileController: Controller
{
    [HttpPost]
    [Route("/profile")]
    public IActionResult AddProfile( [FromBody] ProfileModel profile ) {
        DbProfile.AddProfile(profile);
        return Ok();
    }

    [HttpGet]
    [Route("/profile")]
    public IActionResult GetProfile() {
        // var profile = DbProfile.GetProfileById(id);
        return View("/Views/Profile.cshtml");
    }

}