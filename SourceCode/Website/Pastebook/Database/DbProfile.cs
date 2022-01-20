namespace Database;
using System.Data.SqlClient;
using Models;

public class DbProfile
{
    private static SqlConnection? OpenDatabase()
    {
        try
        {
            string? connection = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            if(connection == null)
            {
                throw new Exception($"database connection null {Environment.NewLine}");
            }
            var db = new SqlConnection(connection);
            db.Open();
            return db;
        }
        catch (System.Exception e)
        {
            System.Console.WriteLine(e.ToString());
        }
        return(null);
    }

    public static void AddProfile(ProfileModel profile)
    {
        SqlConnection? db = OpenDatabase();
        if(db == null) return;
        using(db)
        {
            using(var command = db.CreateCommand())
            {
                command.CommandText = 
                    @"INSERT INTO Profiles (UserId, ProfilePicture, About, FriendsList)
                    VALUES (@UserId, @ProfilePicture, @About, @FriendsList)";
                command.Parameters.AddWithValue("@UserId", profile.UserId);
                command.Parameters.AddWithValue("@ProfilePicture", profile.ProfilePicture);
                command.Parameters.AddWithValue("@About", profile.About);
                command.Parameters.AddWithValue("@FriendsList", profile.FriendsList);
                command.ExecuteNonQuery();
            }
        }
    }

    public static ProfileModel? GetProfileById(int id)
    {
        ProfileModel profile = new ProfileModel();

        SqlConnection? db = OpenDatabase();
        if(db == null) return null;
        using(db)
        {
            using (var command = db.CreateCommand()) 
            {
                command.CommandText = "SELECT * FROM Profiles WHERE Id = @Id;";
                command.Parameters.AddWithValue("@Id", id);
                var reader = command.ExecuteReader();
                if(!reader.HasRows) return null;
                while(reader.Read()) {
                    profile.UserId = reader.GetInt32(0);
                    profile.ProfilePicture = reader.GetString(1);
                    profile.About = reader.GetString(2);
                    profile.FriendsList = reader.GetString(3);
                }
            }
        }
        return profile;
    }


}