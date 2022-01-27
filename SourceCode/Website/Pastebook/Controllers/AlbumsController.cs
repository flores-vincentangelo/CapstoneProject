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

    [HttpGet]
    [Route("/albums/{email}")]
    public IActionResult GetAllAlbums(string email) {
        var albums = DbAlbums.GetAllAlbums(email);
        if(albums == null) {
            return Ok("No Albums found");
        }
        return Json(albums);
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

    // Album Photos

    [HttpPost]
    [Route("/photos/{albumId}")]
    public IActionResult AddPhotoInAlbumId(int albumId, [FromBody] PhotoModel photo) {
        string? cookieEmail = HttpContext.Request.Cookies["email"];
        string? cookieSessionId = HttpContext.Request.Cookies["sessionId"];


        photo.UserEmail = cookieEmail;
        // photo.Photo = base64image from body
        photo.UploadDate = (long)((System.DateTime.Now.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds);
        photo.AlbumId = albumId;
        photo.Likes = "";
        photo.Comments = "";
        DbPhotos.AddPhotoInAlbumId(albumId, photo);
        // Get Photo List with AlbumId = albumId
        var photoList = DbPhotos.GetPhotoList(albumId);
        // Update Album PhotoList
        DbAlbums.UpdatePhotoList(albumId, photoList.ToString());
        
        return Ok("Photo added in album successfully!");
    }

    [HttpGet]
    [Route("/photos/{albumId}")]
    public IActionResult GetAllPhotosByAlbumId() {
        var albumId = 1; // must take from html
        var photos = DbPhotos.GetAllPhotosByAlbumId(albumId);
        if(photos == null){
            return Ok("No photo found");
        }
        return Json(photos);
    }

    [HttpGet]
    [Route("/photos")]
    public IActionResult GetPhotoById() {
        var photoId = 1; // must take from html
        var photo = DbPhotos.GetPhotoByPhotoId(photoId);
        if(photo == null){
            return Ok("No photo found");
        }
        return Json(photo);
    }

    

}