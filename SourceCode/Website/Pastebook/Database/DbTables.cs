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
                    FullName VARCHAR (255),
                    Duplicate INTEGER,
                    ProfileLink VARCHAR (255),
                    ProfileName VARCHAR (255),
                    About VARCHAR(MAX),
                    Photo VARCHAR(MAX),
                    Cover VARCHAR(MAX)
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
                    Id VARCHAR(255) NOT NULL,
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
    public static void CreateProfilesTable()
    {     
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                command.CommandText = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Profiles' and xtype='U')
                CREATE TABLE Profiles(
                    Id VARCHAR(255),
                    FullName VARCHAR(255),
                    About VARCHAR(MAX),
                    Photo VARCHAR(MAX),
                    Cover VARCHAR(MAX)
                );";
                command.ExecuteNonQuery();
                Console.WriteLine("Profiles Table created successfully!");
             }
        }
    }

    public static void DropProfilesTable()
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                command.CommandText = "DROP TABLE IF EXISTS Profiles";
                command.ExecuteNonQuery();
                Console.WriteLine("Profiles Table deleted successfully!");
            }
        }
    }

    public static void CreateFriendsTable()
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                command.CommandText = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Friends' and xtype='U')
                CREATE TABLE Friends (
                    UserEmail VARCHAR(255) NOT NULL PRIMARY KEY,
                    FriendsList TEXT,
                    FriendRequests TEXT
                    );";
                command.ExecuteNonQuery();
                Console.WriteLine("Friends Table created successfully!");
            }
        }
    }

    public static void DropFriendsTable()
    {
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var command = db.CreateCommand())
            {
                command.CommandText = "DROP TABLE IF EXISTS Friends";
                command.ExecuteNonQuery();
                Console.WriteLine("Friends Table deleted successfully!");
            }
        }
    }

    public static void CreateAlbumsTable()
    {     
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                command.CommandText = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Albums' and xtype='U')
                CREATE TABLE Albums (
                    AlbumId INTEGER NOT NULL IDENTITY (1,1) PRIMARY KEY,
                    AlbumName VARCHAR (255),
                    UserEmail VARCHAR (255),
                    CreatedDate BIGINT
                    );";
                command.ExecuteNonQuery();
                Console.WriteLine("Albums Table created successfully!");
             }
        }
    }

    public static void DropAlbumsTable()
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                command.CommandText = "DROP TABLE IF EXISTS Albums";
                command.ExecuteNonQuery();
                Console.WriteLine("Albums Table deleted successfully!");
            }
        }
    }

    public static void CreatePhotosTable()
    {     
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                command.CommandText = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Photos' and xtype='U')
                CREATE TABLE Photos (
                    PhotoId INTEGER NOT NULL IDENTITY (1,1) PRIMARY KEY,
                    UserEmail VARCHAR (255),
                    Photo VARCHAR (MAX),
                    UploadDate BIGINT,
                    AlbumId VARCHAR (255),
                    Likes VARCHAR (MAX),
                    Comments VARCHAR (MAX)
                    );";
                command.ExecuteNonQuery();
                Console.WriteLine("Photos Table created successfully!");
             }
        }
    }

    public static void DropPhotosTable()
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                command.CommandText = "DROP TABLE IF EXISTS Photos";
                command.ExecuteNonQuery();
                Console.WriteLine("Photos Table deleted successfully!");
            }
        }
    }

    public static void CreatePostsTable()
    {     
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                command.CommandText = @"IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Posts' and xtype='U')
                CREATE TABLE Posts (
                    EmailAddress VARCHAR (255),
                    PostId INTEGER NOT NULL IDENTITY (1,1) PRIMARY KEY,
                    DatePosted BIGINT,
                    Caption VARCHAR (255),
                    PhotoId INTEGER NOT NULL IDENTITY (1,1) PRIMARY KEY,
                    Photo VARCHAR (MAX),
                    Like VARCHAR (MAX),
                    Comment VARCHAR (MAX),
                    PostLink VARCHAR (255)
                    );";
                command.ExecuteNonQuery();
                Console.WriteLine("Posts Table created successfully!");
             }
        }
    }

    public static void DropPostsTable()
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                command.CommandText = "DROP TABLE IF EXISTS Posts";
                command.ExecuteNonQuery();
                Console.WriteLine("Posts Table deleted successfully!");
            }
        }
    }
}