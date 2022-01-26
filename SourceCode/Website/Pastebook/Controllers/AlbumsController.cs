namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

public class AlbumsController: Controller
{
    [HttpPost]
    [Route("/albums")]
    public IActionResult AddAlbum([FromBody] AlbumModel album) {
        album.CreatedDate = (long)((System.DateTime.Now.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds);
        album.PhotosList = "";
        DbAlbums.InsertAlbum(album);
        return Ok("Album created successfully");
    }

    [HttpGet]
    [Route("/albums")]
    public IActionResult GetAllAlbums() {
        string? cookieEmail = HttpContext.Request.Cookies["email"];
        string? cookieSessionId = HttpContext.Request.Cookies["sessionId"];
        if(cookieSessionId != null)
        {
            SessionsModel? sessionModel = DbSessions.GetSessionById(cookieSessionId);
            if(sessionModel != null)
            {
                var albums = DbAlbums.GetAllAlbums(cookieEmail);
                if(albums == null) {
                    return Ok("No Albums found");
                }
                return Json(albums);
            }
        }
        return Ok();
    }

    [HttpPatch]
    [Route("/albums/{id}")]
    public IActionResult Modify(int id,  [FromBody] AlbumModel album) {
        DbAlbums.Modify(id, album);
        // var updatedAlbum = DbAlbums.GetAlbumById(id);
        return Ok("Album updated successfully!");
    }

    [HttpDelete]
    [Route("/albums")]
    public IActionResult DeleteAlbum([FromBody] AlbumModel album) {
        DbAlbums.DeleteAlbumByName(album.AlbumName);
        // var updatedAlbum = DbAlbums.GetAlbumById(id);
        return Ok("Album deleted successfully!");
    }

}