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

    public static void InsertPhoto(PhotoModel photo)
    {
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText =
                    @"INSERT INTO Photos (UserEmail, Photo, UploadDate, AlbumId, Likes, Comments) 
                    VALUES (@UserEmail, @Photo, @UploadDate, @AlbumId, @Likes, @Comments);";
                cmd.Parameters.AddWithValue("@UserEmail", photo.UserEmail);
                cmd.Parameters.AddWithValue("@Photo", photo.Photo);
                cmd.Parameters.AddWithValue("@UploadDate", photo.UploadDate);
                cmd.Parameters.AddWithValue("@AlbumId", photo.AlbumId);
                cmd.Parameters.AddWithValue("@Likes", photo.Likes);
                cmd.Parameters.AddWithValue("@Comments", photo.Comments);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Photo successfully added to Photos Table!");
            }
        }
    }

    public static void DeletePhotoById(int id)
    {
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Photos WHERE PhotoId = @PhotoId;";
                cmd.Parameters.AddWithValue("@PhotoId", id);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void DeletePhotoByAlbum(int albumId)
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

}