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
                    @"INSERT INTO Albums (UserEmail, Name, CreatedDate) 
                    VALUES (@UserEmail, @Name, @CreatedDate);";
                cmd.Parameters.AddWithValue("@UserEmail", album.UserEmail);
                cmd.Parameters.AddWithValue("@Name", album.Name);
                cmd.Parameters.AddWithValue("@CreatedDate", album.CreatedDate);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Album successfully added to Albums Table!");
            }
        }
    }

}