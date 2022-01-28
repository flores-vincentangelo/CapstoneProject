namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

public class AlbumsController: Controller
{
    [HttpPost]
    [Route("/albums")]
    public IActionResult AddAlbum([FromBody] AlbumModel album) {
        string? profileLink = HttpContext.Request.Cookies["profilelink"];
        album.CreatedDate = (long)((System.DateTime.Now.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds);
        album.PhotosList = "";
        album.ProfileLink = profileLink;
        DbAlbums.InsertAlbum(album);
        return Ok("Album created successfully");
    }

    [HttpGet]
    [Route("/albums")]
    public IActionResult GetAllAlbumsByEmail() {
        string? cookieEmail = HttpContext.Request.Cookies["email"];
        string? cookieSessionId = HttpContext.Request.Cookies["sessionId"];
        if(cookieSessionId != null)
        {
            SessionsModel? sessionModel = DbSessions.GetSessionById(cookieSessionId);
            if(sessionModel != null)
            {
                var albums = DbAlbums.GetAllAlbumsByEmail(cookieEmail);
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
        var albums = DbAlbums.GetAllAlbumsByEmail(email);
        if(albums == null) {
            return Ok("No Albums found");
        }
        return Json(albums);
    }

    [HttpPatch]
    [Route("/albums/{id}")]
    public IActionResult ModifyAlbumName(int id,  [FromBody] AlbumModel album) {
        DbAlbums.ModifyAlbumName(id, album.AlbumName);
        return Ok("Album name updated successfully!");
    }

    [HttpDelete]
    [Route("/albums/{id}")]
    public IActionResult DeleteAlbumByAlbumId(int id) {
        DbAlbums.DeleteAlbumByAlbumId(id);
        DbPhotos.DeletePhotoByAlbumId(id);
        return Ok("Album deleted successfully!");
    }

    // Album Photos

    [HttpPost]
    [Route("/photos/{albumId}")]
    public IActionResult AddPhotoInAlbumId(int albumId, [FromBody] PhotoModel photo) {
        string? cookieEmail = HttpContext.Request.Cookies["email"];
        string? cookieSessionId = HttpContext.Request.Cookies["sessionId"];
        string? profileLink = HttpContext.Request.Cookies["profilelink"];
        

        photo.UserEmail = cookieEmail;
        // photo.Photo = base64image from body
        photo.UploadDate = (long)((System.DateTime.Now.Subtract(new System.DateTime(1970, 1, 1))).TotalSeconds);
        photo.AlbumId = albumId;
        photo.ProfileLink = profileLink;
        photo.Likes = "";
        photo.Comments = "";
        DbPhotos.AddPhotoInAlbumId(albumId, photo);
        // Get Photo List with AlbumId = albumId
        var photoList = DbPhotos.GetPhotoListByAlbumId(albumId);
        
        if(photoList != null) {
            string photoListString = string.Join( ",", photoList);
            Console.WriteLine("-------------------");
            Console.WriteLine($"There are {photoList.Count} photos in Album Id {albumId}");
            Console.WriteLine(photoListString);
            Console.WriteLine("-------------------");
            
            // Update Album PhotoList
            DbAlbums.UpdatePhotoList(albumId, photoListString);
        }
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