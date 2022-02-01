namespace Controllers;
using Microsoft.AspNetCore.Mvc; 
using Database;
using Models;

public class RegisterController : Controller
{
    [HttpGet]
    [Route("/register")]
    public IActionResult GetRegisterAction() {
        return View("/Views/Register/Register.cshtml");
    }
    
    [HttpPost]
    [Route("/register")]
    public IActionResult doPostRegistration()
    {
        var firstName = HttpContext.Request.Form["FirstName"];
        var lastName = HttpContext.Request.Form["LastName"];
        var emailAddress = HttpContext.Request.Form["EmailAddress"];
        var mobileNumber = HttpContext.Request.Form["MobileNumber"];
        var password = HttpContext.Request.Form["Password"];
        var birthday = HttpContext.Request.Form["Birthday"];
        DateTime birthDate = DateTime.Parse(birthday);
        var dateOfBirth = (long)((birthDate.Subtract(new System.DateTime(1970, 1, 1, 0, 0, 0, 0))).TotalSeconds);
        var gender = HttpContext.Request.Form["Gender"];
        
        var model = new UserModel();
        var isEmailExists = DbUsers.checkEmailAddress(emailAddress);
        if (isEmailExists)
        {
            return View("Views/Register/EmailExists.cshtml");
        }

        model.FirstName = firstName;
        model.LastName = lastName;
        model.EmailAddress = emailAddress;
        model.MobileNumber = mobileNumber;
        model.Password = password;
        model.Birthday = dateOfBirth;
        model.Gender = gender;
        model.FullName = ((firstName + lastName).Replace(" ", "")).ToLower();
        var duplicate = DbUsers.checkFullName(model.FullName);
        if(duplicate == -1)
        {
            duplicate = 0;
        }
        else 
        { 
            duplicate += 1; 
        }

        model.Duplicate = duplicate;
        model.ProfileLink = model.FullName + model.Duplicate;
        model.ProfileName =  firstName + " " + lastName;
        model.About = "Write something about me";
        model.Cover = "";
        byte[] imageArray = System.IO.File.ReadAllBytes("./wwwroot/Images/img_avatar.png");
        string base64ImageRepresentation = Convert.ToBase64String(imageArray);
        model.Photo = "data:image/png;base64," + base64ImageRepresentation;

        DbUsers.InsertUser(model);
        DbUsers.SendVerificationEmail(model);

        int userId = DbUsers.GetUserByEmail(model.EmailAddress).UserId;

        FriendsModel? isFriendsInitialized = DbFriends.GetFriendsData(userId);
        var isNotificationsInitialized = DbNotifications.GetNotificationsByUserId(userId);
        if(isFriendsInitialized == null)
        {
            DbFriends.InitializeFriends(userId);
        }

        if(isNotificationsInitialized == null)
        {
            DbNotifications.InitializeNotifications(userId);
        }
        return View("Views/Register/RegisteredSuccess.cshtml");
    }
}