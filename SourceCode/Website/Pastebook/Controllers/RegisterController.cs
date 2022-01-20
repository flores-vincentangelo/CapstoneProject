namespace Controllers;
using Microsoft.AspNetCore.Mvc;

public class RegisterController : Controller
{
    [HttpGet]
    [Route("/register")]
    public IActionResult GetRegisterAction() {
        return View("/Views/Register.cshtml");
    }

    [HttpPost]
    [Route("/register")]
    public IActionResult PostRegistration()
    {
        var firstName = HttpContext.Request.Form["FirstName"];
        var lastName = HttpContext.Request.Form["LastName"];
        var email = HttpContext.Request.Form["Email"];
        var mobileNumber = HttpContext.Request.Form["MobileNumber"];
        var password = HttpContext.Request.Form["Password"];
        var birthday = HttpContext.Request.Form["Birthday"];
        DateTime birthDate = DateTime.Parse(birthday);
        var dateOfBirth = (long)((birthDate.Subtract(new System.DateTime(1970, 1, 1, 0, 0, 0, 0))).TotalSeconds);
        var gender = HttpContext.Request.Form["Gender"];

        var model = new UserModel();
        model.FirstName = firstName;
        model.LastName = lastName;
        model.Email = email;
        model.MobileNumber = mobileNumber;
        model.Password = password;
        model.Birthday = dateOfBirth;
        model.Gender = gender;
        Database.DBRegister.InsertUser(model);
        return View("Views/RegisteredSuccess.cshtml");
    }
}