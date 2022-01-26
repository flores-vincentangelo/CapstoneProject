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
                    @"INSERT INTO Albums (UserEmail, AlbumName, CreatedDate, PhotosList) 
                    VALUES (@UserEmail, @AlbumName, @CreatedDate, @PhotosList);";
                cmd.Parameters.AddWithValue("@UserEmail", album.UserEmail);
                cmd.Parameters.AddWithValue("@AlbumName", album.AlbumName);
                cmd.Parameters.AddWithValue("@CreatedDate", album.CreatedDate);
                cmd.Parameters.AddWithValue("@PhotosList", album.PhotosList);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Album successfully added to Albums Table!");
            }
        }
    }

    public static List<AlbumModel>? GetAllAlbums(string email)
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
                    albums.Add(album);
                }
            }
        }
        return albums;
    }

    public static AlbumModel? GetAlbumById(int id)
    {
        AlbumModel album = new AlbumModel();
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Albums WHERE AlbumId = @AlbumId;";
                cmd.Parameters.AddWithValue("@AlbumId", id);
                var reader = cmd.ExecuteReader();
                if(!reader.HasRows) return null;
                while(reader.Read()) {
                    album.AlbumId = reader.GetInt32(0);
                    album.AlbumName = reader.GetString(1);
                    album.UserEmail = reader.GetString(2);
                    album.CreatedDate = reader.GetInt64(3);
                    album.PhotosList = reader.GetString(4);
                }
            }
        }
        return album;
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
                }
            }
        }
        return album;
    }

    public static void DeleteAlbumByName(string albumName)
    {
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Albums WHERE AlbumName = @AlbumName;";
                cmd.Parameters.AddWithValue("@AlbumName", albumName);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void Modify(int id, AlbumModel album)
    {
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            if(!String.IsNullOrEmpty(album.UserEmail))
            {
                using (var cmd = db.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Albums SET UserEmail = @UserEmail WHERE AlbumId = @AlbumId;";
                    cmd.Parameters.AddWithValue("@UserEmail", album.UserEmail);
                    cmd.Parameters.AddWithValue("@AlbumId", id);
                    cmd.ExecuteNonQuery();
                }
            }
            if(!String.IsNullOrEmpty(album.AlbumName))
            {
                using (var cmd = db.CreateCommand())    
                {
                    cmd.CommandText = "UPDATE Albums SET AlbumName = @AlbumName WHERE AlbumId = @AlbumId;";
                    cmd.Parameters.AddWithValue("@AlbumName", album.AlbumName);
                    cmd.Parameters.AddWithValue("@AlbumId", id);
                    cmd.ExecuteNonQuery();
                }
            }
            if(!String.IsNullOrEmpty(album.PhotosList))
            {
                using (var cmd = db.CreateCommand())    
                {
                    cmd.CommandText = "UPDATE Albums SET PhotosList = @PhotosList WHERE AlbumId = @AlbumId;";
                    cmd.Parameters.AddWithValue("@PhotosList", album.PhotosList);
                    cmd.Parameters.AddWithValue("@AlbumId", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

}