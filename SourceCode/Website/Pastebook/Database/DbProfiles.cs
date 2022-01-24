namespace Database;
using System.Data.SqlClient;
using Models;

public class DbProfiles
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
                    @"INSERT INTO Profiles (Id, FullName, About, Photo, Cover)
                    VALUES (@Id, @FullName, @About, @Photo, @Cover)";
                command.Parameters.AddWithValue("@Id", profile.Id);
                command.Parameters.AddWithValue("@FullName", profile.FullName);
                command.Parameters.AddWithValue("@About", profile.About);
                command.Parameters.AddWithValue("@Photo", profile.Photo);
                command.Parameters.AddWithValue("@Cover", profile.Cover);
                command.ExecuteNonQuery();
            }
        }
    }

    public static ProfileModel? GetProfileById(string id)
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
                    profile.Id = reader.GetString(0);
                    profile.FullName = reader.GetString(1);
                    profile.About = reader.GetString(2);
                    profile.Photo = reader.GetString(3);
                    profile.Cover = reader.GetString(4);
                }
            }
        }
        return profile;
    }

    public static void DeleteProfile(string id)
    {
        SqlConnection? db = OpenDatabase();
        if(db == null) return;
        using(db)
        {
            using (var command = db.CreateCommand()) 
            {
                command.CommandText = $"DELETE FROM Profiles WHERE Id = @Id;";
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }

        }
    }

    public static void ModifyProfile(string id, ProfileModel profile)
    {
        SqlConnection? db = OpenDatabase();
        if(db == null) return;
        using(db)
        {
            using(var command = db.CreateCommand())
            {
                if(!String.IsNullOrEmpty(profile.FullName))
                {
                    command.CommandText = "UPDATE Profiles SET FullName = @FullName WHERE Id = @Id;";
                    command.Parameters.AddWithValue("@FullName", profile.FullName);
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
                if(!String.IsNullOrEmpty(profile.About))
                {
                    command.CommandText = "UPDATE Profiles SET About = @About WHERE Id = @Id;";
                    command.Parameters.AddWithValue("@About", profile.About);
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
                if(!String.IsNullOrEmpty(profile.Photo))
                {
                    command.CommandText = "UPDATE Profiles SET Photo = @Photo WHERE Id = @Id;";
                    command.Parameters.AddWithValue("@Photo", profile.Photo);
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
                if(!String.IsNullOrEmpty(profile.Cover))
                {
                    command.CommandText = "UPDATE Profiles SET Cover = @Cover WHERE Id = @Id;";
                    command.Parameters.AddWithValue("@Cover", profile.Cover);
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
    
}