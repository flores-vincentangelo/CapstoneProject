namespace Database;
using System.Data.SqlClient;
using System.Text; 
using Models;

public class DbPhotos
{
    private static string? DB_CONNECTION_STRING;

    static DbPhotos()
    {
        DB_CONNECTION_STRING = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
    }

    public static void AddPhotoInAlbumId(int albumId, PhotoModel photo)
    {
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText =
                    @"INSERT INTO Photos (UserEmail, Photo, UploadDate, AlbumId, UserId, PostId, ProfileLink, Likes, Comments) 
                    VALUES (@UserEmail, @Photo, @UploadDate, @AlbumId, @UserId, @PostId, @ProfileLink, @Likes, @Comments);";
                cmd.Parameters.AddWithValue("@UserEmail", photo.UserEmail);
                cmd.Parameters.AddWithValue("@Photo", photo.Photo);
                cmd.Parameters.AddWithValue("@UploadDate", photo.UploadDate);
                cmd.Parameters.AddWithValue("@AlbumId", photo.AlbumId);
                cmd.Parameters.AddWithValue("@UserId", photo.UserId);
                cmd.Parameters.AddWithValue("@PostId", photo.PostId);
                cmd.Parameters.AddWithValue("@ProfileLink", photo.ProfileLink);
                cmd.Parameters.AddWithValue("@Likes", photo.Likes);
                cmd.Parameters.AddWithValue("@Comments", photo.Comments);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Photo successfully added to Photos Table!");
            }
        }
    }

    public static PhotoModel? GetPhotoByPhotoId(int photoId)
    {
        PhotoModel photo = new PhotoModel();
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Photos WHERE PhotoId = @PhotoId;";
                cmd.Parameters.AddWithValue("@PhotoId", photoId);
                var reader = cmd.ExecuteReader();
                if(!reader.HasRows) return null;
                while(reader.Read()) {
                    photo.PhotoId = reader.GetInt32(0);
                    photo.UserEmail = reader.GetString(1);
                    photo.Photo = reader.GetString(2);
                    photo.UploadDate = reader.GetInt64(3);
                    photo.AlbumId = reader.GetInt32(4);
                    photo.UserId = reader.GetInt32(5);
                    photo.PostId = reader.GetInt32(6);
                    photo.ProfileLink = reader.GetString(7);
                    photo.Likes = reader.GetString(8);
                    photo.Comments = reader.GetString(9);
                }
            }
        }
        return photo;
    }

    public static List<PhotoModel>? GetAllPhotosByAlbumId(int albumId)
    {
        List<PhotoModel> photos = new List<PhotoModel>();
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Photos WHERE AlbumId = @AlbumId;";
                cmd.Parameters.AddWithValue("@AlbumId", albumId);
                var reader = cmd.ExecuteReader();
                if(!reader.HasRows) return null;
                while(reader.Read()) {
                    PhotoModel photo = new PhotoModel();
                    photo.PhotoId = reader.GetInt32(0);
                    photo.UserEmail = reader.GetString(1);
                    photo.Photo = reader.GetString(2);
                    photo.UploadDate = reader.GetInt64(3);
                    photo.AlbumId = reader.GetInt32(4);
                    photo.UserId = reader.GetInt32(5);
                    photo.PostId = reader.GetInt32(6);
                    photo.ProfileLink = reader.GetString(7);
                    photo.Likes = reader.GetString(8);
                    photo.Comments = reader.GetString(9);
                    photos.Add(photo);
                }
            }
        }
        return photos;
    }

    public static List<PhotoModel>? GetAllPhotosByProfileLink(string profileLink)
    {
        List<PhotoModel> photos = new List<PhotoModel>();
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Photos WHERE ProfileLink = @ProfileLink;";
                cmd.Parameters.AddWithValue("@ProfileLink", profileLink);
                var reader = cmd.ExecuteReader();
                if(!reader.HasRows) return null;
                while(reader.Read()) {
                    PhotoModel photo = new PhotoModel();
                    photo.PhotoId = reader.GetInt32(0);
                    photo.UserEmail = reader.GetString(1);
                    photo.Photo = reader.GetString(2);
                    photo.UploadDate = reader.GetInt64(3);
                    photo.AlbumId = reader.GetInt32(4);
                    photo.UserId = reader.GetInt32(5);
                    photo.PostId = reader.GetInt32(6);
                    photo.ProfileLink = reader.GetString(7);
                    photo.Likes = reader.GetString(8);
                    photo.Comments = reader.GetString(9);
                    photos.Add(photo);
                }
            }
        }
        return photos;
    }

    public static List<PhotoModel>? GetAllPhotosByUserId(int userId)
    {
        List<PhotoModel> photos = new List<PhotoModel>();
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Photos WHERE UserId = @UserId;";
                cmd.Parameters.AddWithValue("@UserId", userId);
                var reader = cmd.ExecuteReader();
                if(!reader.HasRows) return null;
                while(reader.Read()) {
                    PhotoModel photo = new PhotoModel();
                    photo.PhotoId = reader.GetInt32(0);
                    photo.UserEmail = reader.GetString(1);
                    photo.Photo = reader.GetString(2);
                    photo.UploadDate = reader.GetInt64(3);
                    photo.AlbumId = reader.GetInt32(4);
                    photo.UserId = reader.GetInt32(5);
                    photo.PostId = reader.GetInt32(6);
                    photo.ProfileLink = reader.GetString(7);
                    photo.Likes = reader.GetString(8);
                    photo.Comments = reader.GetString(9);
                    photos.Add(photo);
                }
            }
        }
        return photos;
    }

    public static List<int>? GetPhotoListByAlbumId(int albumId)
    {
        List<int> photolist = new List<int>();
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = "SELECT PhotoId FROM Photos WHERE AlbumId = @AlbumId;";
                cmd.Parameters.AddWithValue("@AlbumId", albumId);
                var reader = cmd.ExecuteReader();
                if(!reader.HasRows) return null;
                while(reader.Read()) {
                    photolist.Add(reader.GetInt32(0));
                }
            }
        }
        return photolist;
    }

    public static List<int>? GetPhotoListByProfileLink(string profileLink)
    {
        List<int> photolist = new List<int>();
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = "SELECT PhotoId FROM Photos WHERE ProfileLink = @ProfileLink;";
                cmd.Parameters.AddWithValue("@ProfileLink", profileLink);
                var reader = cmd.ExecuteReader();
                if(!reader.HasRows) return null;
                while(reader.Read()) {
                    photolist.Add(reader.GetInt32(0));
                }
            }
        }
        return photolist;
    }

    public static void DeletePhotoByPhotoId(int photoId)
    {
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Photos WHERE PhotoId = @PhotoId;";
                cmd.Parameters.AddWithValue("@PhotoId", photoId);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void DeletePhotoByAlbumId(int albumId)
    {
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Photos WHERE AlbumId = @AlbumId;";
                cmd.Parameters.AddWithValue("@AlbumId", albumId);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void UpdatePostIdByPhotoId(int? photoId, int? postId)
    {
        PhotoModel photo = new PhotoModel();
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = "UPDATE Photos SET PostId = @PostId WHERE PhotoId = @PhotoId;";
                cmd.Parameters.AddWithValue("@PostId", photoId);
                cmd.Parameters.AddWithValue("@PhotoId", postId);
                cmd.ExecuteNonQuery();
            }
        }
    }

}