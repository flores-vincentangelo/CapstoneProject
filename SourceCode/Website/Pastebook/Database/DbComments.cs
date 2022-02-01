namespace Database;
using System.Data.SqlClient;
using Models;
public class DbComments
{
    private static string? DB_CONNECTION_STRING;

    static DbComments()
    {
        DB_CONNECTION_STRING = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
    }

    public static List<CommentsModel>? GetCommentsByPost(int? postId)
    {
        List<CommentsModel> commentsList = new List<CommentsModel>();
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText = 
                    @"SELECT * from Comments WHERE PostId = @postid;";
                cmd.Parameters.AddWithValue("@postid", postId);
                var reader = cmd.ExecuteReader();
                if(!reader.HasRows) return null;
                while(reader.Read())
                {
                    CommentsModel comment = new CommentsModel();
                    int commenterId = reader.GetInt32(0);
                    comment.CommenterUserModel = DbUsers.GetUserById(commenterId);
                    comment.CommentId = reader.GetInt32(2);
                    comment.CommentText = reader.GetString(3);
                    commentsList.Add(comment);
                }
            }
        }
        return commentsList;
    }

    public static void AddCommentToPost(CommentsModel model)
    {
        using(var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using(var cmd = db.CreateCommand())
            {
                cmd.CommandText =
                    @"INSERT INTO Comments
                    (UserId, PostId, CommentText)
                    VALUES
                    (@email,@postid,@text);";
                cmd.Parameters.AddWithValue("@email",model.CommenterId);
                cmd.Parameters.AddWithValue("@postid",model.PostId);
                cmd.Parameters.AddWithValue("@text",model.CommentText);
                cmd.ExecuteNonQuery();
            }
        }
    }
}