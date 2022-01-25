$(document).ready(() => {
    EditProfile();
});

function EditProfile() {
    // When the user clicks on the "Edit" button,
    $('#profile-edit-btn').click(() => {
        // show edit form 
        $('.profile-edit-modal').css("display","flex");
    });

    // When the user clicks on the "x",
    $('#profile-close-modal').click(() => {
        // Close modal
        $('.profile-edit-modal').css("display","none");
    });
    
    // When the user clicks on the "Cancel" button,
    $('#cancel-btn').click(() => {
        // hide edit form 
        $('.profile-edit-modal').css("display","none");
    });
    
    // When the user clicks on the "Save" button,
    $('#save-btn').click((event) => {
        // Change "Details"
        var gender = document.getElementById("gender");
        $('#profile-gender').text(gender.value);
        // Save gender.value to database
        var formData = new FormData(document.getElementById('form-profile'));
        var data = JSON.stringify(Object.fromEntries(formData.entries()));
        modifyProfileData(event, data);
        // hide edit form
        $('.profile-edit').css("display","none");
    });
}

async function modifyProfileData(event, jsonData) {
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
        alert("Profile modified successfully!");
        var userData = await response.json();
        localStorage.setItem('User', JSON.stringify(userData));
    }
}
