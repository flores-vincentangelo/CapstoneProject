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
            DbTables.CreateFriendsTable(); //added by Vincent
            DbTables.CreateAlbumsTable(); //added by JP
            DbTables.CreatePhotosTable(); //added by JP
            DbTables.CreatePostsTable(); //added by Jen
            DbTables.CreateNotificationsTable();
            DbTables.CreateCommentsTable();
            return Ok("All Tables are Created!");
        }
        else if (config == "DropTables") {
            DbTables.DropUsersTable();
            DbTables.DropSessionsTable();
            DbTables.DropFriendsTable(); //added by Vincent
            DbTables.DropAlbumsTable(); //added by JP
            DbTables.DropPhotosTable(); //added by JP
            DbTables.DropPostsTable(); //added by Jen
            DbTables.DropNotificationsTable();
            DbTables.DropCommentsTable();
            return Ok("All Tables are Dropped");
        }
        else
        {
            return Ok("Input 'CreateTables' or 'DropTables' only");
        }
    }
}