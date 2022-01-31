namespace Database;
using System.Data.SqlClient;
using System.Text; 
using Models;

public class DbAlbums
{
    private static string? DB_CONNECTION_STRING;

    static DbAlbums()
    {
        DB_CONNECTION_STRING = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
    }

    public static void InsertAlbum(AlbumModel album)
    {
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText =
                    @"INSERT INTO Albums (UserEmail, AlbumName, CreatedDate, PhotosList, ProfileLink, UserId) 
                    VALUES (@UserEmail, @AlbumName, @CreatedDate, @PhotosList, @ProfileLink, @UserId);";
                cmd.Parameters.AddWithValue("@UserEmail", album.UserEmail);
                cmd.Parameters.AddWithValue("@AlbumName", album.AlbumName);
                cmd.Parameters.AddWithValue("@CreatedDate", album.CreatedDate);
                cmd.Parameters.AddWithValue("@PhotosList", album.PhotosList);
                cmd.Parameters.AddWithValue("@ProfileLink", album.ProfileLink);
                cmd.Parameters.AddWithValue("@UserId", album.UserId);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Album successfully added to Albums Table!");
            }
        }
    }

    public static List<AlbumModel>? GetAllAlbumsByEmail(string email)
    {
        List<AlbumModel> albums = new List<AlbumModel>();
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Albums WHERE UserEmail = @UserEmail;";
                cmd.Parameters.AddWithValue("@UserEmail", email);
                var reader = cmd.ExecuteReader();
                if(!reader.HasRows) return null;
                while(reader.Read()) {
                    AlbumModel album = new AlbumModel();
                    album.AlbumId = reader.GetInt32(0);
                    album.AlbumName = reader.GetString(1);
                    album.UserEmail = reader.GetString(2);
                    album.CreatedDate = reader.GetInt64(3);
                    album.PhotosList = reader.GetString(4);
                    album.ProfileLink = reader.GetString(5);
                    album.UserId = reader.GetInt32(6);
                    albums.Add(album);
                }
            }
        }
        return albums;
    }

    public static List<AlbumModel>? GetAllAlbumsByProfileLink(string profileLink)
    {
        List<AlbumModel> albums = new List<AlbumModel>();
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Albums WHERE ProfileLink = @ProfileLink;";
                cmd.Parameters.AddWithValue("@ProfileLink", profileLink);
                var reader = cmd.ExecuteReader();
                if(!reader.HasRows) return null;
                while(reader.Read()) {
                    AlbumModel album = new AlbumModel();
                    album.AlbumId = reader.GetInt32(0);
                    album.AlbumName = reader.GetString(1);
                    album.UserEmail = reader.GetString(2);
                    album.CreatedDate = reader.GetInt64(3);
                    album.PhotosList = reader.GetString(4);
                    album.ProfileLink = reader.GetString(5);
                    album.UserId = reader.GetInt32(6);
                    albums.Add(album);
                }
            }
        }
        return albums;
    }

    public static List<AlbumModel>? GetAllAlbumsByUserId(int userId)
    {
        List<AlbumModel> albums = new List<AlbumModel>();
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Albums WHERE UserId = @UserId;";
                cmd.Parameters.AddWithValue("@UserId", userId);
                var reader = cmd.ExecuteReader();
                if(!reader.HasRows) return null;
                while(reader.Read()) {
                    AlbumModel album = new AlbumModel();
                    album.AlbumId = reader.GetInt32(0);
                    album.AlbumName = reader.GetString(1);
                    album.UserEmail = reader.GetString(2);
                    album.CreatedDate = reader.GetInt64(3);
                    album.PhotosList = reader.GetString(4);
                    album.ProfileLink = reader.GetString(5);
                    album.UserId = reader.GetInt32(6);
                    albums.Add(album);
                }
            }
        }
        return albums;
    }

    public static AlbumModel? GetAlbumByAlbumId(int albumId)
    {
        AlbumModel album = new AlbumModel();
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Albums WHERE AlbumId = @AlbumId;";
                cmd.Parameters.AddWithValue("@AlbumId", albumId);
                var reader = cmd.ExecuteReader();
                if(!reader.HasRows) return null;
                while(reader.Read()) {
                    album.AlbumId = reader.GetInt32(0);
                    album.AlbumName = reader.GetString(1);
                    album.UserEmail = reader.GetString(2);
                    album.CreatedDate = reader.GetInt64(3);
                    album.PhotosList = reader.GetString(4);
                    album.ProfileLink = reader.GetString(5);
                    album.UserId = reader.GetInt32(6);
                }
            }
        }
        return album;
    }

    public static void UpdatePhotoList(int albumId, string photoList)
    {
        AlbumModel album = new AlbumModel();
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = "UPDATE Albums SET PhotosList = @PhotosList WHERE AlbumId = @AlbumId;";
                cmd.Parameters.AddWithValue("@PhotosList", photoList);
                cmd.Parameters.AddWithValue("@AlbumId", albumId);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static AlbumModel? GetAlbumByEmail(string email)
    {
        AlbumModel album = new AlbumModel();
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Albums WHERE UserEmail = @UserEmail;";
                cmd.Parameters.AddWithValue("@UserEmail", email);
                var reader = cmd.ExecuteReader();
                if(!reader.HasRows) return null;
                while(reader.Read()) {
                    album.AlbumId = reader.GetInt32(0);
                    album.AlbumName = reader.GetString(1);
                    album.UserEmail = reader.GetString(2);
                    album.CreatedDate = reader.GetInt64(3);
                    album.UserEmail = reader.GetString(4);
                    album.ProfileLink = reader.GetString(5);
                    album.UserId = reader.GetInt32(6);
                }
            }
        }
        return album;
    }

    public static void DeleteAlbumByAlbumId(int albumId)
    {
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Albums WHERE AlbumId = @AlbumId;";
                cmd.Parameters.AddWithValue("@AlbumId", albumId);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void ModifyAlbumName(int albumId, string albumName)
    {
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())    
            {
                cmd.CommandText = "UPDATE Albums SET AlbumName = @AlbumName WHERE AlbumId = @AlbumId;";
                cmd.Parameters.AddWithValue("@AlbumName", albumName);
                cmd.Parameters.AddWithValue("@AlbumId", albumId);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void AddPhotoIdToPhotosList(int photoId, string photosList)
    {
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = "UPDATE Albums SET PhotosList = @PhotosList WHERE AlbumId = @AlbumId;";
                cmd.Parameters.AddWithValue("@PhotosList", photosList);
                cmd.Parameters.AddWithValue("@AlbumId", photoId);
                cmd.ExecuteNonQuery();
            }    
        }
    }

}