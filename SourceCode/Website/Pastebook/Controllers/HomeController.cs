namespace Controllers;
using Microsoft.AspNetCore.Mvc;

public class HomeController: Controller
{
    [HttpGet]
    [Route("/")]
    public IActionResult GetHome()
    {
        return Ok("Hello World");
    }
}