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
                command.CommandText=@"INSERT INTO Sessions (Id, EmailAddress, LastLogin) VALUES (@Id, @EmailAddress, @LastLogin);";
                command.Parameters.AddWithValue("@Id", session.Id);
                command.Parameters.AddWithValue("@EmailAddress", session.EmailAddress);
                command.Parameters.AddWithValue("@LastLogin", session.LastLogin);
                command.ExecuteNonQuery();
            }
        }
    }    

    public static SessionsModel AddSessionsForUser(string EmailAddress, long LastLogin)
    {
        SessionsModel session = new SessionsModel();
        using (var db = new SqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")))
        {
            db.Open();
            session.Id = Guid.NewGuid().ToString();
            session.EmailAddress = EmailAddress;
            session.LastLogin = LastLogin;
            AddSessions(session);  
        }
        return session;
    }

    public static SessionsModel? AddSessionWithCredentials(string EmailAddress, string Password, long LastLogin)
    {
        var result = false;
        SessionsModel session = new SessionsModel();
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                command.CommandText = "SELECT EmailAddress, Password FROM Users where EmailAddress = @EmailAddress;";
                command.Parameters.AddWithValue("@EmailAddress", EmailAddress);
                var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    result = BCrypt.Net.BCrypt.Verify(Password, reader.GetString(1));
                    System.Console.WriteLine($"{Environment.NewLine} {result} {Environment.NewLine}");
                }
            } 
        }
        if (result)
        {
            session = AddSessionsForUser(EmailAddress, LastLogin);
            return session;
        }
       return null;
    }

    public static SessionsModel? GetSessionById(string Id)
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
                if(!reader.HasRows) return null;
                else
                {
                    while(reader.Read())
                    {
                        session.Id = reader.GetString(0);
                        session.EmailAddress = reader.GetString(1);
                        session.LastLogin = reader.GetInt64(2);
                    }
                }
                
            }
        }
        if(IsSessionExpired(session.LastLogin))
        {
            DeleteSession(session.Id);
            return null;
        }
        else
        {
            return session;
        }
    }

    public static void DeleteSession(string Id)
    {
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

    public static bool IsSessionExpired(long sessionCreationDateUnix)
    {
        DateTime readableUnixDate = new System.DateTime(1970, 1, 1).AddSeconds(sessionCreationDateUnix);
        DateTime now = DateTime.Now;
        DateTime expiryDate = readableUnixDate.AddDays(3);
        if(now >= expiryDate) return true; 
        else return false;
    }
}