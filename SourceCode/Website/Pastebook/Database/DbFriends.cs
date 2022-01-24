namespace Database;
using System.Data.SqlClient;
using Models;

public class DbFriends
{
    private static string? DB_CONNECTION_STRING;

    static DbFriends()
    {
        DB_CONNECTION_STRING = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
    }

    public static void InitializeFriends(string email)
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText = 
                    @"INSERT INTO Friends (UserEmail) VALUES (@email);";
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.ExecuteNonQuery();
                    System.Console.WriteLine("Entry added to Friends Table");
            }
        }
    }

    // public static FriendsModel GetFriendRequests(string email)
    // {
    //     FriendsModel model = new FriendsModel();
    //     using(var db = new SqlConnection(DB_CONNECTION_STRING))
    //     {
    //         db.Open();
    //         using(var cmd = db.CreateCommand())
    //         {
    //             cmd.CommandText = "SELECT FriendRequests FROM Friends WHERE UserEmail = @email;";
    //             cmd.Parameters.AddWithValue("@email", email);
    //             var reader = cmd.ExecuteReader();
    //             while(reader.Read())
    //             {
    //                 model.UserEmail = email;
    //                 model.FriendRequests = reader.GetString(0);
    //             }
    //         }
    //     }
    //     return model;
    // }

    public static FriendsModel GetFriendsData(string email)
    {
        FriendsModel model = new FriendsModel();
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText = "SELECT FriendsList, FriendRequests FROM Friends WHERE UserEmail = @email;";
                cmd.Parameters.AddWithValue("@email",email);
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    model.UserEmail = email;
                    model.FriendsList = reader.GetString(0);
                    model.FriendRequests = reader.GetString(1);
                }
            }
        }
        return model;
    }

    public static void SendFriendRequest(FriendsModel friendsModel)
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText = 
                    @"UPDATE Friends 
                    SET FriendRequests = @friendrequests 
                    WHERE UserEmail = @email;";
                cmd.Parameters.AddWithValue("@friendrequests",friendsModel.FriendRequests);
                cmd.Parameters.AddWithValue("@email",friendsModel.UserEmail);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public static void AddAsFriend(FriendsModel friendsModel)
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText =
                    @"UPDATE Friends
                    SET FriendsList = @friendslist
                    WHERE UserEmail = @email;";
                cmd.Parameters.AddWithValue("@friendslist",friendsModel.FriendsList);
                cmd.Parameters.AddWithValue("@email",friendsModel.UserEmail);
                cmd.ExecuteNonQuery();
            }
        }
    }


}