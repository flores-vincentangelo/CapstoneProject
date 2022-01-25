namespace Controllers;
using Microsoft.AspNetCore.Mvc;
using Database;
using Models;

public class ConfigureController: Controller
{
    [Route("/configure")]
    public IActionResult Configure(
        [FromBody] ConfigureActionModel param) {
        var config = param.Action;
        if(config == "CreateTables") {
            DbTables.CreateUsersTable();
            DbTables.CreateSessionsTable();
            DbTables.CreateFriendsTable(); //added by Vincent
            DbTables.CreateAlbumsTable(); //added by JP
            DbTables.CreatePhotosTable(); //added by JP
            return Ok("All Tables are Created!");
        }
        else if (config == "DropTables") {
            DbTables.DropUsersTable();
            DbTables.DropSessionsTable();
            DbTables.DropFriendsTable(); //added by Vincent
            DbTables.DropAlbumsTable(); //added by JP
            DbTables.DropPhotosTable(); //added by JP
            return Ok("All Tables are Dropped");
        }
        else if(config == "MakeUsers")
        {
            UserModel user1 = new UserModel();
            user1.FirstName = "Decca";
            user1.LastName = "Minthorpe";
            user1.EmailAddress = "dminthorpe0@virginia.edu";
            user1.MobileNumber = "09479051271";
            user1.Password = "123";
            DateTime birthDate = DateTime.Parse("1998/09/08");
            var dateOfBirth = (long)((birthDate.Subtract(new System.DateTime(1970, 1, 1, 0, 0, 0, 0))).TotalSeconds);
            user1.Birthday = dateOfBirth;
            user1.Gender = "Female";
            user1.FullName = ((user1.FirstName + user1.LastName).Replace(" ", "")).ToLower();
            var duplicate1 = DbUsers.checkFullName(user1.FullName);
            if(duplicate1 == -1)
            {
                duplicate1 = 0;
            }
            else 
            { 
                duplicate1 += 1; 
            }
            user1.Duplicate = duplicate1;
            user1.ProfileLink = user1.FullName + user1.Duplicate;
            var isEmailExists = DbUsers.checkEmailAddress(user1.EmailAddress);
            if (!isEmailExists)
            {
                DbFriends.InitializeFriends(user1.EmailAddress);
                DbUsers.InsertUser(user1);
            }

            UserModel user2 = new UserModel();
            user2.FirstName = "Judith";
            user2.LastName = "Danter";
            user2.EmailAddress = "jdanter0@booking.com";
            user2.MobileNumber = "09479051271";
            user2.Password = "123";
            birthDate = DateTime.Parse("1998/09/08");
            dateOfBirth = (long)((birthDate.Subtract(new System.DateTime(1970, 1, 1, 0, 0, 0, 0))).TotalSeconds);
            user2.Birthday = dateOfBirth;
            user2.Gender = "Female";
            user2.FullName = ((user2.FirstName + user2.LastName).Replace(" ", "")).ToLower();
            duplicate1 = DbUsers.checkFullName(user2.FullName);
            if(duplicate1 == -1)
            {
                duplicate1 = 0;
            }
            else 
            { 
                duplicate1 += 1; 
            }
            user2.Duplicate = duplicate1;
            user2.ProfileLink = user2.FullName + user2.Duplicate;
            isEmailExists = DbUsers.checkEmailAddress(user2.EmailAddress);
            if (!isEmailExists)
            {
                DbFriends.InitializeFriends(user2.EmailAddress);
                DbUsers.InsertUser(user2);
            }

            UserModel user3 = new UserModel();
            user3.FirstName = "Baudoin";
            user3.LastName = "Chardin";
            user3.EmailAddress = "bchardin1@chron.com";
            user3.MobileNumber = "09479051271";
            user3.Password = "123";
            birthDate = DateTime.Parse("1998/09/08");
            dateOfBirth = (long)((birthDate.Subtract(new System.DateTime(1970, 1, 1, 0, 0, 0, 0))).TotalSeconds);
            user3.Birthday = dateOfBirth;
            user3.Gender = "Female";
            user3.FullName = ((user3.FirstName + user3.LastName).Replace(" ", "")).ToLower();
            duplicate1 = DbUsers.checkFullName(user3.FullName);
            if(duplicate1 == -1)
            {
                duplicate1 = 0;
            }
            else 
            { 
                duplicate1 += 1; 
            }
            user3.Duplicate = duplicate1;
            user3.ProfileLink = user3.FullName + user3.Duplicate;
            isEmailExists = DbUsers.checkEmailAddress(user3.EmailAddress);
            if (!isEmailExists)
            {
                DbFriends.InitializeFriends(user3.EmailAddress);
                DbUsers.InsertUser(user3);
            }

            UserModel user4 = new UserModel();
            user4.FirstName = "Delila";
            user4.LastName = "Pascall";
            user4.EmailAddress = "dpascall2@unblog.fr";
            user4.MobileNumber = "09479051271";
            user4.Password = "123";
            birthDate = DateTime.Parse("1998/09/08");
            dateOfBirth = (long)((birthDate.Subtract(new System.DateTime(1970, 1, 1, 0, 0, 0, 0))).TotalSeconds);
            user4.Birthday = dateOfBirth;
            user4.Gender = "Male";
            user4.FullName = ((user4.FirstName + user4.LastName).Replace(" ", "")).ToLower();
            duplicate1 = DbUsers.checkFullName(user4.FullName);
            if(duplicate1 == -1)
            {
                duplicate1 = 0;
            }
            else 
            { 
                duplicate1 += 1; 
            }
            user4.Duplicate = duplicate1;
            user4.ProfileLink = user4.FullName + user4.Duplicate;
            isEmailExists = DbUsers.checkEmailAddress(user4.EmailAddress);
            if (!isEmailExists)
            {
                DbFriends.InitializeFriends(user4.EmailAddress);
                DbUsers.InsertUser(user4);
            }

            UserModel user5 = new UserModel();
            user5.FirstName = "Charlean";
            user5.LastName = "Reidie";
            user5.EmailAddress = "creidie3@plala.or.jp";
            user5.MobileNumber = "09479051271";
            user5.Password = "123";
            birthDate = DateTime.Parse("1998/09/08");
            dateOfBirth = (long)((birthDate.Subtract(new System.DateTime(1970, 1, 1, 0, 0, 0, 0))).TotalSeconds);
            user5.Birthday = dateOfBirth;
            user5.Gender = "Female";
            user5.FullName = ((user5.FirstName + user5.LastName).Replace(" ", "")).ToLower();
            duplicate1 = DbUsers.checkFullName(user5.FullName);
            if(duplicate1 == -1)
            {
                duplicate1 = 0;
            }
            else 
            { 
                duplicate1 += 1; 
            }
            user5.Duplicate = duplicate1;
            user5.ProfileLink = user5.FullName + user5.Duplicate;
            isEmailExists = DbUsers.checkEmailAddress(user5.EmailAddress);
            if (!isEmailExists)
            {
                DbFriends.InitializeFriends(user5.EmailAddress);
                DbUsers.InsertUser(user5);
            }

            
            
            
            return Ok();
        } 
        else
        {
            return Ok("Input 'CreateTables' or 'DropTables' only");
        }
    }
}