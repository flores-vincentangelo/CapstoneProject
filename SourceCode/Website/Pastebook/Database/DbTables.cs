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
                command.CommandText=  command.CommandText= @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users' and xtype='U')
                CREATE TABLE Users (
                    UserId INTEGER NOT NULL IDENTITY (1,1) PRIMARY KEY,
                    Password VARCHAR (255),
                    FirstName VARCHAR (255),
                    LastName VARCHAR (255),
                    EmailAddress VARCHAR (255),
                    Birthday BIGINT,
                    Gender VARCHAR (255),
                    MobileNumber VARCHAR (255)
                    );";
                command.ExecuteNonQuery();
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
                command.CommandText=  command.CommandText= @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Sessions' and xtype='U')
                CREATE TABLE Sessions (
                    Id VARCHAR(255) NOT NULL PRIMARY KEY,
                    EmailAddress VARCHAR (255),
                    LastLogin BIGINT
                    );";
                command.ExecuteNonQuery();
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
            }
        }
    }
}