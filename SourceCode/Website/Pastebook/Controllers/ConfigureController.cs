namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

public class ConfigureController: Controller
{
    [Route("/configure")]
    public IActionResult Configure(
        [FromBody] ConfigureActionModel param) {
        var config = param.Action;
        if(config == "CreateTables") {
            DbTables.CreateUsersTable();
            DbTables.CreateSessionsTable();
            return Ok("All Tables are Created!");
        }
        else if (config == "DropTables") {
            DbTables.DropUsersTable();
            DbTables.DropSessionsTable();
            return Ok("All Tables are Dropped");
        }
        else {
            return Ok("Input 'CreateTables' or 'DropTables' only");
        }
    }
}