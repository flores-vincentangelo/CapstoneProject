namespace Database;
using Models;
using System.Data.SqlClient;

public class DbUsers
{
    private static string? DB_CONNECTION_STRING;

    static DbUsers()
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
                    @"INSERT INTO Users (FirstName, LastName, EmailAddress, MobileNumber, Password, Birthday, Gender) 
                    VALUES (@FirstName, @LastName, @EmailAddress, @MobileNumber, @Password, @Birthday, @Gender);";
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@EmailAddress", user.EmailAddress);
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