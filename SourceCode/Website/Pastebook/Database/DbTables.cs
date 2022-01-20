namespace Database;
using System.Data.SqlClient;

public class DbTables
{
    private static string? DB_CONNECTION_STRING;

    static DbTables()
    {
        DB_CONNECTION_STRING = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
    }

    public static void CreateUsersTable()
    {     
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                command.CommandText = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users' and xtype='U')
                CREATE TABLE Users (
                    UserId INTEGER NOT NULL IDENTITY (1,1) PRIMARY KEY,
                    FirstName VARCHAR (255),
                    LastName VARCHAR (255),
                    EmailAddress VARCHAR (255),
                    MobileNumber VARCHAR (255),
                    Password VARCHAR (255),
                    Birthday BIGINT,
                    Gender VARCHAR (255),
                    );";
                command.ExecuteNonQuery();
                Console.WriteLine("Users Table created successfully!");
             }
        }
    }

    public static void DropUsersTable()
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                command.CommandText = "DROP TABLE IF EXISTS Users";
                command.ExecuteNonQuery();
                Console.WriteLine("Users Table deleted successfully!");
            }
        }
    }

    public static void CreateSessionsTable()
    {     
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                command.CommandText = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Sessions' and xtype='U')
                CREATE TABLE Sessions (
                    Id VARCHAR(255) NOT NULL PRIMARY KEY,
                    EmailAddress VARCHAR (255),
                    LastLogin BIGINT
                    );";
                command.ExecuteNonQuery();
                Console.WriteLine("Sessions Table created successfully!");
             }
        }
    }

    public static void DropSessionsTable()
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                command.CommandText = "DROP TABLE IF EXISTS Sessions";
                command.ExecuteNonQuery();
                Console.WriteLine("Sessions Table deleted successfully!");
            }
        }
    }
}