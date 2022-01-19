namespace Controllers;
using Microsoft.AspNetCore.Mvc;

public class MainController: Controller
{
    [HttpGet]
    [Route("/")]
    public IActionResult LandingPage()
    {
        return Ok("Hello World");
    }
}