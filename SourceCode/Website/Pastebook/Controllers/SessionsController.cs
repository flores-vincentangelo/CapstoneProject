namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

public class SessionsController: Controller
{
    [HttpPost]
    [Route("/sessions")]
    public IActionResult AddSession ([FromBody] UserCredentialsModel userCredentials)
    {
        var session = DbSessions.AddSessionWithCredentials(userCredentials.EmailAddress, userCredentials.Password);
        if (session == null) {
            return Unauthorized();
        }
        else {
            return (Json(session));
        }
    }

    [HttpDelete]
    [Route("/sessions/{id}")]
    public IActionResult DeleteSession(string Id)
    {
        DbSessions.DeleteSession(Id);
        return Ok("Session Deleted!");
    }
}