namespace Database;
using System.Data.SqlClient;

public class DBRegister
{
    private static string? DB_CONNECTION_STRING;

    static DBRegister()
    {
        DB_CONNECTION_STRING = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
    }

    public static void InsertUser(UserModel user)
    {
        using (var db = new SqlConnection(DB_CONNECTION_STRING))
        {
            db.Open();
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText =
                    @"INSERT INTO Users (FirstName, LastName, Email, MobileNumber, Password, Birthday, Gender) 
                    VALUES (@FirstName, @LastName, @Email, @MobileNumber, @Password, @Birthday, @Gender);";
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@MobileNumber", user.MobileNumber);
                cmd.Parameters.AddWithValue("@Password", BCrypt.Net.BCrypt.HashPassword(user.Password));
                cmd.Parameters.AddWithValue("@Birthday", user.Birthday);
                cmd.Parameters.AddWithValue("@Gender", user.Gender);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Form successfully added to Users Table!");
            }
        }
    }
}