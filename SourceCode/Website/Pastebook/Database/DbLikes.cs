namespace Database;
using System.Data.SqlClient;
using Models;
using Database;

public class DbLikes
{
    private static string? DB_CONNECTION_STRING;

    static DbLikes()
    {
        DB_CONNECTION_STRING = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
    }
  
    public static void UpdateLikesListOfPost(int postId, string? likesList)
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var command = db.CreateCommand())
            {
                command.CommandText = 
                    @"UPDATE Posts
                    SET LikesList = @likesList
                    WHERE PostId = @postId;";
                if(String.IsNullOrEmpty(likesList))
                {
                    command.Parameters.AddWithValue("@likesList", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@likesList", likesList);
                }
                command.Parameters.AddWithValue("@postId", postId);
                command.ExecuteNonQuery();
            }
        }
    }

    public static string AddEmailtoLikesList(string emailToAdd, string? likesListStr)
    {
        if(!String.IsNullOrEmpty(likesListStr))
        {
            var _likesListArr = likesListStr.Split(",");
            List<string> likesList = new List<string>(_likesListArr);
            likesList.Add(emailToAdd);
            return String.Join(",",likesList);
        }
        else
        {
            return emailToAdd;
        }
    }

    public static List<UserModel>? GetListAsUserObj (string? list)
    {
        List<UserModel> userListObj = new List<UserModel>();
        if(String.IsNullOrEmpty(list))
        {
            return null;
        }
        else
        {
            var emailArr = list.Split(',');
            foreach (string email in emailArr)
            {
                UserModel userModel = DbUsers.GetUserByEmail(email);
                userListObj.Add(userModel);
            }
            return userListObj;
        }
    }

    public static bool IsUserInLikersList(string cookieEmail, string? likesList)
    {
        if(String.IsNullOrEmpty(likesList))
        {
            return false;
        }
        else
        {
            return likesList.Contains(cookieEmail);
        }
        
    }
}