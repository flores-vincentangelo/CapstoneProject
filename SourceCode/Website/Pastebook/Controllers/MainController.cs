namespace Controllers;
using Microsoft.AspNetCore.Mvc;

public class MainController: Controller
{
    [Route("/configure")]
    public IActionResult Configure(
        [FromBody] ConfigureActionModel param, 
        [FromHeader (Name = "X-Apikey")] string apiKey) {
        var config = param.Action;
        var correctApiKey = new string (Environment.GetEnvironmentVariable("ADMIN_API_KEY"));
        if (apiKey != correctApiKey) {
            return Unauthorized();
        }
        if(config == "CreateTables") {
            DbMain.CreateTables();
            return Ok("All Tables are Created!");
        }
        else if (config == "DropTables") {
            DbMain.DropTables();
            return Ok("All Tables are Dropped");
        }
        else {
            return Ok("Input 'CreateTables' or 'DropTables' only");
        }
    }
}