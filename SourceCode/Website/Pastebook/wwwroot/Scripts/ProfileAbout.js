$(document).ready(() => {
    EditFirstName();
    EditLastName();
    EditEmailAddress();
    EditMobileNumber();
    EditBirthday();
    EditGender();
    EditPassword();
});

function EditFirstName() {
    // When the user clicks on the "Edit" button,
    $('#profile-edit-firstname-btn').click(() => {
        // Show edit form 
        $('.profile-edit-firstname-modal').css("display", "flex");
    });

    // When the user clicks on the "x" button,
    $('#profile-close-firstname-modal').click(() => {
        // Close modal
        $('.profile-edit-firstname-modal').css("display", "none");
    });

    // When the user clicks on the "Cancel" button,
    $('#firstname-cancel-btn').click(() => {
        // Hide edit form 
        $('.profile-edit-firstname-modal').css("display", "none");
    });

    // When the user clicks on the "Save" button,
    $('#firstname-save-btn').click((event) => {
        // Change "First Name"
        var firstName = document.getElementById("first-name");
        $('#model-firstname-details').text(firstName.value);
        // Save firstName.value to database
        var formData = new FormData(document.getElementById('form-profile-firstname'));
        var data = JSON.stringify(Object.fromEntries(formData.entries()));
        modifyDetails(event, data);
    });
}

function EditLastName() {
    // When the user clicks on the "Edit" button,
    $('#profile-edit-lastname-btn').click(() => {
        // Show edit form 
        $('.profile-edit-lastname-modal').css("display", "flex");
    });

    // When the user clicks on the "x" button,
    $('#profile-close-lastname-modal').click(() => {
        // Close modal
        $('.profile-edit-lastname-modal').css("display", "none");
    });

    // When the user clicks on the "Cancel" button,
    $('#lastname-cancel-btn').click(() => {
        // Hide edit form 
        $('.profile-edit-lastname-modal').css("display", "none");
    });

    // When the user clicks on the "Save" button,
    $('#lastname-save-btn').click((event) => {
        // Change "Last Name"
        var lastName = document.getElementById("last-name");
        $('#model-lastname-details').text(lastName.value);
        // Save lastName.value to database
        var formData = new FormData(document.getElementById('form-profile-lastname'));
        var data = JSON.stringify(Object.fromEntries(formData.entries()));
        modifyDetails(event, data);
    });
}

function EditEmailAddress() {
    // When the user clicks on the "Edit" button,
    $('#profile-edit-email-btn').click(() => {
        // Show edit form 
        $('.profile-edit-email-modal').css("display", "flex");
    });

    // When the user clicks on the "x" button,
    $('#profile-close-email-modal').click(() => {
        // Close modal
        $('.profile-edit-email-modal').css("display", "none");
    });

    // When the user clicks on the "Cancel" button,
    $('#email-cancel-btn').click(() => {
        // Hide edit form 
        $('.profile-edit-email-modal').css("display", "none");
    });

    // When the user clicks on the "Save" button,
    $('#email-save-btn').click((event) => {
        // Change "Email Address"
        var email = document.getElementById("email");
        $('#model-email-details').text(email.value);
        // Save email.value to database
        var formData = new FormData(document.getElementById('form-profile-email'));
        var data = JSON.stringify(Object.fromEntries(formData.entries()));
        modifyEmail(event, data);
        // Delete session
        deleteSession();

        //delete cookies
        document.cookie = "email=; expires=Thu, 01 Jan 1970 00:00:00 UTC;";
        document.cookie = "sessionId=; expires=Thu, 01 Jan 1970 00:00:00 UTC;";
        document.cookie = "profilelink=; expires=Thu, 01 Jan 1970 00:00:00 UTC;";

        //delete Local Storage
        localStorage.clear();
    });
}

function EditMobileNumber() {
    // When the user clicks on the "Edit" button,
    $('#profile-edit-mobile-btn').click(() => {
        // show edit form 
        $('.profile-edit-mobile-modal').css("display", "flex");
    });

    // When the user clicks on the "x" button,
    $('#profile-close-mobile-modal').click(() => {
        // Close modal
        $('.profile-edit-mobile-modal').css("display", "none");
    });

    // When the user clicks on the "Cancel" button,
    $('#mobile-cancel-btn').click(() => {
        // hide edit form 
        $('.profile-edit-mobile-modal').css("display", "none");
    });

    // When the user clicks on the "Save" button,
    $('#mobile-save-btn').click((event) => {
        // Change "Mobile Number"
        var mobile = document.getElementById("mobile");
        $('#model-mobile-details').text(mobile.value);
        // Save mobile.value to database
        var formData = new FormData(document.getElementById('form-profile-mobile'));
        var data = JSON.stringify(Object.fromEntries(formData.entries()));
        modifyDetails(event, data);
    });
}

function EditBirthday() {
    // Disable future dates
    var date = new Date();
    var tdate = date.getDate(); 
    var month = date.getMonth() + 1;
    if (tdate < 10) {
        tdate = '0' + tdate;
    }
    if (month < 10) {
        month = '0' + month;
    }
    var year = date.getUTCFullYear();
    var maxDate = year + "-" + month + "-" + tdate;
    $("#birthday").attr("max", maxDate);

    // When the user clicks on the "Edit" button,
    $('#profile-edit-birthday-btn').click(() => {
        // show edit form 
        $('.profile-edit-birthday-modal').css("display", "flex");
    });

    // When the user clicks on the "x" button,
    $('#profile-close-birthday-modal').click(() => {
        // Close modal
        $('.profile-edit-birthday-modal').css("display", "none");
    });

    // When the user clicks on the "Cancel" button,
    $('#birthday-cancel-btn').click(() => {
        // hide edit form 
        $('.profile-edit-birthday-modal').css("display", "none");
    });

    // When the user clicks on the "Save" button,
    $('#birthday-save-btn').click((event) => {
        // Change "Birthday"
        var birthday = document.getElementById("birthday");
        $('#model-birthday-details').text(birthday.value);
        // Save birthday.value to database
        var formData = new FormData(document.getElementById('form-profile-birthday'));
        var data = JSON.stringify(Object.fromEntries(formData.entries()));
        modifyDetails(event, data);
    });
}

function EditGender() {
    // When the user clicks on the "Edit" button,
    $('#profile-edit-gender-btn').click(() => {
        // show edit form 
        $('.profile-edit-gender-modal').css("display", "flex");
    });

    // When the user clicks on the "x" button,
    $('#profile-close-gender-modal').click(() => {
        // Close modal
        $('.profile-edit-gender-modal').css("display", "none");
    });

    // When the user clicks on the "Cancel" button,
    $('#gender-cancel-btn').click(() => {
        // hide edit form 
        $('.profile-edit-gender-modal').css("display", "none");
    });

    // When the user clicks on the "Save" button,
    $('#gender-save-btn').click((event) => {
        // Change "Gender"
        var gender = document.getElementById("gender");
        $('#model-gender-details').text(gender.value);
        // Save gender.value to database
        var formData = new FormData(document.getElementById('form-profile-gender'));
        var data = JSON.stringify(Object.fromEntries(formData.entries()));
        modifyDetails(event, data);
    });
}

function EditPassword() {
    // When the user clicks on the "Edit" button,
    $('#profile-edit-password-btn').click(() => {
        // show edit form 
        $('.profile-edit-password-modal').css("display", "flex");
    });

    // When the user clicks on the "x" button,
    $('#profile-close-password-modal').click(() => {
        // Close modal
        $('.profile-edit-password-modal').css("display", "none");
    });

    // When the user clicks on the "Cancel" button,
    $('#password-cancel-btn').click(() => {
        // hide edit form 
        $('.profile-edit-password-modal').css("display", "none");
    });

    // When the user clicks on the "Save" button,
    $('#password-save-btn').click((event) => {
        // Save password.value to database
        var formData = new FormData(document.getElementById('form-profile-password'));
        var data = JSON.stringify(Object.fromEntries(formData.entries()));
        modifyPassword(event, data);
    });
}

async function modifyDetails(event, jsonData) {
    event.preventDefault();
    const link = localStorage.getItem('profileLink');
    const response = await fetch(`/${link}`, {
        method: 'PATCH',
        headers: {
            'Content-Type': 'application/json'
        },
        body: jsonData
    });
    if (response.status == 200) {
        location.reload();
    }
}

async function modifyPassword(event, jsonData) {
    event.preventDefault();
    var newPassword = document.getElementById('new-password').value;
    var confirmNewPassword = document.getElementById('confirm-new-password').value;
    const link = localStorage.getItem('profileLink');
    if (newPassword == confirmNewPassword) {
        const response = await fetch(`/${link}`, {
            method: 'PATCH',
            headers: {
                'Content-Type': 'application/json'
            },
            body: jsonData
        });
        if (response.status == 200) {
            alert("Password successfully changed!");
            location.reload();
        }
        else {
            alert("Wrong password. Try again.")
        }
    }
    else {
        alert("Passwords do not match! Try again.");
    }
}

async function modifyEmail(event, jsonData) {
    event.preventDefault();
    var inputPassword = document.getElementById('password').value;
    var confirmPassword = document.getElementById('confirm-password').value;
    const link = localStorage.getItem('profileLink');
    if (inputPassword == confirmPassword) {
        const response = await fetch(`/${link}`, {
            method: 'PATCH',
            headers: {
                'Content-Type': 'application/json'
            },
            body: jsonData
        });
        if (response.status == 200) {
            alert("Email Address successfully changed!");
            window.location.replace("/login");
        }
        else {
            alert("Wrong password. Try again.")
        }
    }
    else {
        alert("Passwords do not match! Try again.");
    }
}

async function deleteSession() {  
    await fetch(`/login`, {
        method: 'DELETE',
    });
}

// LENGTH OF USER'S INPUT RESTRICTION
function MobileNumLimit(element) {
    var max_chars = 10;
    if(element.value.length > max_chars) {
        element.value = element.value.substr(0, max_chars);
    }
}

function PasswordLimit(element) {
    var max_chars = 11;
    if(element.value.length > max_chars) {
        element.value = element.value.substr(0, max_chars);
        alert("Password should have a maximum of 12 characters only!");
    }
}

// LETTER INPUT RESTRICTION
function LettersInput(input) {
    var regex = /[^a-z ]/gi;
    input.value = input.value.replace(regex,"");
}

// NUMBER INPUT RESTRICTION
function NumbersInput(input) {
    var regex = /[^0-9]/g;
    input.value = input.value.replace(regex,"");
}