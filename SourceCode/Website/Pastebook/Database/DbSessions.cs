namespace Database;
using System.Data.SqlClient;
using Models;

public class DbSessions
{
    private static string? DB_CONNECTION_STRING;

    static DbSessions()
    {
        DB_CONNECTION_STRING = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
    }

    public static void AddSessions(SessionsModel session)
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {       
                command.CommandText=@"INSERT INTO Sessions (Id, EmailAddress) VALUES (@Id, @EmailAddress);";
                command.Parameters.AddWithValue("@Id", session.Id);
                command.Parameters.AddWithValue("@EmailAddress", session.EmailAddress);
                command.ExecuteNonQuery();
            }
        }
    }    

    public static SessionsModel AddSessionsForUser(string EmailAddress)
    {
        SessionsModel session = new SessionsModel();
        session.Id = Guid.NewGuid().ToString();
        session.EmailAddress = EmailAddress;
        AddSessions(session);
        return session;
    }

    public static SessionsModel AddSessionWithCredentials(string EmailAddress, string Password)
    {
        SessionsModel session = new SessionsModel();
        var result = false;
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                command.CommandText = $"SELECT EmailAddress, Password FROM Users where EmailAddress = '{EmailAddress}';";
                var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    result = BCrypt.Net.BCrypt.Verify(Password, reader.GetString(1));
                }
            } 
            if (result)
            {
                session = AddSessionsForUser(EmailAddress);
                return session;
            }
        }
       return null;
    }

    public static SessionsModel GetSessionById(string Id)
    {
        SessionsModel session = new SessionsModel();
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Sessions WHERE Id = @Id;";
                command.Parameters.AddWithValue("@Id",Id);
                var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    session.Id = reader.GetString(0);
                    session.EmailAddress = reader.GetString(1);
                }
            }
        }
        return session;
    }

    public static void DeleteSession(string Id)
    {
        SessionsModel session = new SessionsModel();
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                command.CommandText = "DELETE FROM Sessions WHERE Id = @Id;";
                command.Parameters.AddWithValue("@Id",Id);
                command.ExecuteNonQuery(); 
            }
        }
    }
}