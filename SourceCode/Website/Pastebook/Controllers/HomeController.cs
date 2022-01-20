namespace Controllers;
using Microsoft.AspNetCore.Mvc;

public class HomeController: Controller
{
    [HttpGet]
    [Route("/")]
    public IActionResult GetHome()
    {
        System.Console.WriteLine("hi");
        return Ok("Hello World");
    }
}