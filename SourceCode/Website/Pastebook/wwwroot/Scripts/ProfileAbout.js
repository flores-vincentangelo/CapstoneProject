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
        // show edit form 
        $('.profile-edit-firstname-modal').css("display", "flex");
    });

    // When the user clicks on the "x",
    $('#profile-close-firstname-modal').click(() => {
        // Close modal
        $('.profile-edit-firstname-modal').css("display", "none");
    });

    // When the user clicks on the "Cancel" button,
    $('#firstname-cancel-btn').click(() => {
        // hide edit form 
        $('.profile-edit-firstname-modal').css("display", "none");
    });

    // When the user clicks on the "Save" button,
    $('#firstname-save-btn').click((event) => {
        // Change "First Name"
        var firstName = document.getElementById("first-name");
        $('#profile-first-name').text("First Name: " + firstName.value);
        // Save firstName.value to database
        var formData = new FormData(document.getElementById('form-profile-firstname'));
        var data = JSON.stringify(Object.fromEntries(formData.entries()));
        modifyDetails(event, data);
        // hide edit form
        $('.profile-edit-firstname').css("display", "none");
    });
}

function EditLastName() {
    // When the user clicks on the "Edit" button,
    $('#profile-edit-lastname-btn').click(() => {
        // show edit form 
        $('.profile-edit-lastname-modal').css("display", "flex");
    });

    // When the user clicks on the "x",
    $('#profile-close-lastname-modal').click(() => {
        // Close modal
        $('.profile-edit-lastname-modal').css("display", "none");
    });

    // When the user clicks on the "Cancel" button,
    $('#lastname-cancel-btn').click(() => {
        // hide edit form 
        $('.profile-edit-lastname-modal').css("display", "none");
    });

    // When the user clicks on the "Save" button,
    $('#lastname-save-btn').click((event) => {
        // Change "Last Name"
        var lastName = document.getElementById("last-name");
        $('#profile-last-name').text("Last Name: " + lastName.value);
        // Save lastName.value to database
        var formData = new FormData(document.getElementById('form-profile-lastname'));
        var data = JSON.stringify(Object.fromEntries(formData.entries()));
        modifyDetails(event, data);
        // hide edit form
        $('.profile-edit-lastname').css("display", "none");
    });
}

function EditEmailAddress() {
    // When the user clicks on the "Edit" button,
    $('#profile-edit-email-btn').click(() => {
        // show edit form 
        $('.profile-edit-email-modal').css("display", "flex");
    });

    // When the user clicks on the "x",
    $('#profile-close-email-modal').click(() => {
        // Close modal
        $('.profile-edit-email-modal').css("display", "none");
    });

    // When the user clicks on the "Cancel" button,
    $('#email-cancel-btn').click(() => {
        // hide edit form 
        $('.profile-edit-email-modal').css("display", "none");
    });

    // When the user clicks on the "Save" button,
    $('#email-save-btn').click((event) => {
        // Change "Email Address"
        var email = document.getElementById("email");
        $('#profile-email').text("Email Address: " + email.value);
        // Save email.value to database
        var formData = new FormData(document.getElementById('form-profile-email'));
        var data = JSON.stringify(Object.fromEntries(formData.entries()));
        modifyEmail(event, data);
        // hide edit form
        $('.profile-edit-email').css("display", "none");
    });
}

function EditMobileNumber() {
    // When the user clicks on the "Edit" button,
    $('#profile-edit-mobile-btn').click(() => {
        // show edit form 
        $('.profile-edit-mobile-modal').css("display", "flex");
    });

    // When the user clicks on the "x",
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
        $('#profile-mobile').text("Mobile Number: " + mobile.value);
        // Save mobile.value to database
        var formData = new FormData(document.getElementById('form-profile-mobile'));
        var data = JSON.stringify(Object.fromEntries(formData.entries()));
        modifyDetails(event, data);
        // hide edit form
        $('.profile-edit-mobile').css("display", "none");
    });
}

function EditBirthday() {
    // When the user clicks on the "Edit" button,
    $('#profile-edit-birthday-btn').click(() => {
        // show edit form 
        $('.profile-edit-birthday-modal').css("display", "flex");
    });

    // When the user clicks on the "x",
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
        $('#profile-birthday').text("Birthdate: " + birthday.value);
        // Save birthday.value to database

        var formData = new FormData(document.getElementById('form-profile-birthday'));
        var data = JSON.stringify(Object.fromEntries(formData.entries()));
        console.log(data);
        modifyDetails(event, data);
        // hide edit form
        $('.profile-edit-birthday').css("display", "none");
    });
}

function EditGender() {
    // When the user clicks on the "Edit" button,
    $('#profile-edit-gender-btn').click(() => {
        // show edit form 
        $('.profile-edit-gender-modal').css("display", "flex");
    });

    // When the user clicks on the "x",
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
        $('#profile-gender').text("Gender: " + gender.value);
        // Save gender.value to database
        var formData = new FormData(document.getElementById('form-profile-gender'));
        var data = JSON.stringify(Object.fromEntries(formData.entries()));
        modifyDetails(event, data);
        // hide edit form
        $('.profile-edit-gender').css("display", "none");
    });
}

function EditPassword() {
    // When the user clicks on the "Edit" button,
    $('#profile-edit-password-btn').click(() => {
        // show edit form 
        $('.profile-edit-password-modal').css("display", "flex");
    });

    // When the user clicks on the "x",
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
        // hide edit form
        $('.profile-edit-password').css("display", "none");
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
        alert("Details successfully modified!");
        var userData = await response.json();
        localStorage.setItem('User', JSON.stringify(userData));
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
            alert("Details successfully modified!");
            var userData = await response.json();
            localStorage.setItem('User', JSON.stringify(userData));
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
            alert("Details successfully modified!");
            var userData = await response.json();
            localStorage.setItem('User', JSON.stringify(userData));
        }
        else {
            alert("Wrong password. Try again.")
        }
    }
    else {
        alert("Passwords do not match! Try again.");
    }
}